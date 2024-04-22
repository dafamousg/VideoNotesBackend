using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using VideoNotesBackend.Data;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Controllers
{
    public static class RouteNames
    {
        public const string GetAllVideos = "GetAll";
        public const string GetVideoById = "GetById";
        public const string CreateVideo = "Create";
    }

    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly VideoNotesContext _context;

        public VideoController(VideoNotesContext context)
        {
            _context = context;
        }

        [HttpGet(RouteNames.GetAllVideos)]
        public ActionResult<Video> GetVideos()
        {
            if (_context.Videos == null)
            {
                return NotFound("No videos found");
            }

            var videos = _context.Videos;

            return Ok(videos);
        }

        [HttpPost(RouteNames.CreateVideo)]
        public async Task<ActionResult<Video>> CreateVideo(Video video)
        {
            if(video == null)
            {
                return NotFound("Video Object missing");
            }

            if(ModelState.IsValid)
            {
                _context.Add(video);
                await _context.SaveChangesAsync();

            }

            return Ok(video);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> Edit(Guid? id)
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

            return Ok(video);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Video>> Edit(Guid? id, [FromBody] VideoEditDto editedVideo)
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

        private static void UpdateVideoProps(Video video, VideoEditDto editedVideo)
        {
            TinyMapper.Bind<VideoEditDto, Video>();
            var newVideo = TinyMapper.Map<Video>(editedVideo);

            var videoProperties = typeof(Video).GetProperties()
                .Where(p => p.Name != nameof(Video.Id));

            foreach (var property in videoProperties)
            {
                var newValue = property.GetValue(newVideo);
                var currentValue = property.GetValue(video);

                if(newValue != null && !newValue.Equals(currentValue))
                {
                    property.SetValue(video, newValue);
                }
            }
        }
    }
}
