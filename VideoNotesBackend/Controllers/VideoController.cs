﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.Helpers;
using VideoNotesBackend.Helpers.Converter;
using VideoNotesBackend.ModelDto.Video;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Controllers
{
    [ApiController]
    [Route("api/Video")]
    public class VideoController : ControllerBase
    {
        private readonly VideoNotesContext _context;

        public VideoController(VideoNotesContext context)
        {
            _context = context;
        }

        [HttpGet(RouteNames.GetAll)]
        public ActionResult<List<VideoDto>> Get()
        {
            if (_context.Videos == null)
            {
                return NotFound("No videos found");
            }

            var videos = _context.Videos
                .Include(v => v.Tags)
                .Include(v => v.Rating)
                .Include(v => v.Notes)
                .ToList();

            var returnVideos = Converter.TypeToDto<List<Video>, List<VideoDto>>(videos);

            return Ok(returnVideos);
        }

        [HttpGet(RouteNames.GetById)]
        public async Task<ActionResult<VideoDto>> GetById(Guid? id)
        {
            if (id == null)
            {
                return NotFound("Id is null");
            }

            var video = await _context.Videos
                .Include(v => v.Tags)
                .Include(v => v.Rating)
                .Include(v => v.Notes)
                .SingleOrDefaultAsync(v => v.Id == id);

            if (video == null)
            {
                return NotFound("Video not found");
            }

            var returnVideo = Converter.TypeToDto<Video, VideoDto>(video);

            return Ok(returnVideo);
        }

        [HttpPost(RouteNames.Create)]
        public async Task<ActionResult<VideoDto>> Create(VideoCreate video)
        {
            if (video == null)
            {
                return BadRequest("Video Object missing");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newVideo = Converter.TypeToDto<VideoCreate, Video>(video);


            if (video.Rating != null)
            {
                var rating = await _context.Ratings.SingleOrDefaultAsync(r => r.Id == video.Rating.Id);

                if (rating == null)
                {
                    return BadRequest("Invalid rating value.");
                }

                newVideo.Rating = rating;
            }

            if (video.Notes != null)
            {
                var noteList = new List<Note>();
                foreach (var note in video.Notes)
                {
                    var n = _context.Notes.Find(note.Id);

                    if (n != null)
                    {
                        noteList.Add(n);
                    }
                    else
                    {
                        return NotFound($"No note found with ID: {note.Id}");
                    }
                }

                newVideo.Notes = noteList;
            }

            _context.Add(newVideo);
            await _context.SaveChangesAsync();

            var returnVideo = Converter.TypeToDto<Video, VideoDto>(newVideo);

            return Ok(returnVideo);
        }

        [HttpPost(RouteNames.Update)]
        public async Task<ActionResult<VideoDto>> Update(Guid? id, [FromBody] VideoDto editedVideo)
        {
            if (id == null)
            {
                return NotFound("Id is null");
            }

            var video = await _context.Videos
                .Include(v => v.Tags)
                .Include(v => v.Rating)
                .Include(v => v.Notes)
                .SingleOrDefaultAsync(v => v.Id == id);

            if (video == null)
            {
                return NotFound("Video not found");
            }

            // Compare and Update the video
            await UpdateVideoProps(video, editedVideo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound("Video could not be saved");
            }

            var returnVideo = Converter.TypeToDto<Video, VideoDto>(video);

            return Ok(returnVideo);
        }

        [HttpDelete(RouteNames.Delete)]
        public async Task<ActionResult<string>> Delete(Guid? id)
        {
            var video = await _context.Videos
                .Include(v => v.Notes)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (video == null)
            {
                return NotFound($"Could not find Video with ID: {id}");
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return Ok("Video deleted successfully");
        }

        private async Task UpdateVideoProps(Video video, VideoDto editedVideo)
        {
            var newVideo = Converter.TypeToDto<VideoDto, Video>(editedVideo);

            if (newVideo.Tags != null)
            {
                newVideo.Tags = [.. await Trackers.AssociateChildEntitiesAsync(newVideo.Tags, _context)];
            }
            if (newVideo.Notes != null)
            {
                newVideo.Notes = [.. await Trackers.AssociateChildEntitiesAsync(newVideo.Notes, _context)];
            }
            if (newVideo.Rating != null)
            {
                newVideo.Rating = await Trackers.AssociateChildEntityAsync(newVideo.Rating, _context);
            }

            var videoProperties = typeof(Video).GetProperties()
                .Where(p => p.Name != nameof(Video.Id));

            foreach (var property in videoProperties)
            {
                var newValue = property.GetValue(newVideo);
                var currentValue = property.GetValue(video);


                // Checks if the prop is non-value type      
                var isNullable = !property.PropertyType.IsValueType ||
                    // Checks if the property is a nullable Value type
                    Nullable.GetUnderlyingType(property.PropertyType) != null;

                bool isNullableEntry = isNullable || (!isNullable && newValue != null);
                bool valueCheck = (newValue == null && currentValue != null) ||
                    (newValue != null && !newValue.Equals(currentValue));

                if (isNullableEntry && valueCheck)
                {
                    property.SetValue(video, newValue);
                }
            }
        }
    }
}
