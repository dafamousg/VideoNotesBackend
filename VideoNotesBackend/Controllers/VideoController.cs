using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.Helpers.Validation;
using VideoNotesBackend.ModelDto;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Controllers
{
    [ApiController]
    [Route("api/video")]
    public class VideoController : ControllerBase
    {
        private readonly VideoNotesContext _context;

        public VideoController(VideoNotesContext context)
        {
            _context = context;
        }

        [HttpGet(RouteNames.GetAll)]
        public ActionResult<Video> Get()
        {
            if (_context.Videos == null)
            {
                return NotFound("No videos found");
            }

            var videos = _context.Videos;

            return Ok(videos);
        }

        [HttpPost(RouteNames.Create)]
        public async Task<ActionResult<Video>> Create(VideoCreate video)
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

            TinyMapper.Bind<VideoCreate, Video>();
            var newVideo = TinyMapper.Map<Video>(video);

            _context.Add(newVideo);
            await _context.SaveChangesAsync();

            return Ok(newVideo);
        }


        [HttpGet(RouteNames.GetById)]
        public async Task<ActionResult<Video>> GetById(Guid? id)
        {
            if (id == null)
            {
                return NotFound("Id is null");
            }

            var video = await _context.Videos.FindAsync(id);

            if (video == null)
            {
                return NotFound("Video not found");
            }

            return Ok(video);
        }

        [HttpPost(RouteNames.Edit)]
        public async Task<ActionResult<Video>> Edit(Guid? id, [FromBody] VideoDto editedVideo)
        {
            if (id == null)
            {
                return NotFound("Id is null");
            }

            var video = await _context.Videos.FindAsync(id);

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

            return Ok(video);
        }

        private static void UpdateVideoProps(Video video, VideoDto editedVideo)
        {
            TinyMapper.Bind<VideoDto, Video>();
            var newVideo = TinyMapper.Map<Video>(editedVideo);

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
