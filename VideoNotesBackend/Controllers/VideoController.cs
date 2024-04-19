using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Data;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly VideoNotesContext _context;

        public VideoController(VideoNotesContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetAllVideos")]
        public ActionResult<Video> GetVideos()
        {
            if (_context.Videos == null)
            {
                return NotFound("No videos found");
            }

            var videos = _context.Videos;

            return Ok(videos);
        }

        // Not sure if this works
        [HttpPost("{id}")]
        public async Task<ActionResult<Video>> Edit(Guid? id, [Bind("Id, Title, ReleaseDate, Watched, Duration, Rating, Url")] Video videoDetails)
        {
            if (id == null)
            {
                return NotFound("Id is null");
            }

            var video = await _context.Videos.FindAsync(id);

            if(video == null)
            {
                return NotFound("Video not found");
            }

            // Update the video
            video.Title = videoDetails.Title;
            video.ReleaseDate = videoDetails.ReleaseDate;
            video.Watched = videoDetails.Watched;
            video.Duration = videoDetails.Duration;
            video.Rating = videoDetails.Rating;
            video.URL = videoDetails.URL;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id.Value))
                {
                    return NotFound("Video not found");
                }
                else throw;
            }

            return Ok(video);
        }

        private bool VideoExists(Guid id)
        {
            return _context.Videos.Any(v => v.Id == id);
        }
    }
}
