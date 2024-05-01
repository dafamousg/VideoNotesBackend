using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.Helpers.Converter;
using VideoNotesBackend.Helpers.Validation;
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

            var videos = _context.Videos.Include(v => v.Tags).ToList();

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

            var video = await _context.Videos.Include(v => v.Tags).SingleOrDefaultAsync(v => v.Id == id);

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

            var ratingArray = _context.Ratings;

            if (!RatingValidation.IsValidRating(ratingArray, video.RatingId))
            {
                ModelState.AddModelError(nameof(video.RatingId), $"Invalid rating value: {video.RatingId}");
                return BadRequest(ModelState);
            }

            var newVideo = Converter.TypeToDto<VideoCreate, Video>(video);

            _context.Add(newVideo);
            await _context.SaveChangesAsync();

            var returnVideo = Converter.TypeToDto<Video, VideoDto>(newVideo);

            return Ok(returnVideo);
        }

        [HttpPost(RouteNames.Update)]
        public async Task<ActionResult<VideoDto>> Edit(Guid? id, [FromBody] VideoDto editedVideo)
        {
            if (id == null)
            {
                return NotFound("Id is null");
            }

            var video = await _context.Videos.Include(v => v.Tags).SingleOrDefaultAsync(v => v.Id == id);

            if (video == null)
            {
                return NotFound("Video not found");
            }

            // Compare and Update the video
            UpdateVideoProps(video, editedVideo);

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
            var video = await _context.Videos.FindAsync(id);

            if (video == null)
            {
                return NotFound($"Could not find Video with ID: {id}");
            }
    
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return Ok("Video deleted successfully");
        }

        private static void UpdateVideoProps(Video video, VideoDto editedVideo)
        {
            var newVideo = Converter.TypeToDto<VideoDto, Video>(editedVideo);

            var videoProperties = typeof(Video).GetProperties()
                .Where(p => p.Name != nameof(Video.Id));

            foreach (var property in videoProperties)
            {
                var newValue = property.GetValue(newVideo);
                var currentValue = property.GetValue(video);

                if (newValue != null && !newValue.Equals(currentValue))
                {
                    property.SetValue(video, newValue);
                }
            }
        }
    }
}
