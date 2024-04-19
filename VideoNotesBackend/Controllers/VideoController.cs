using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if(_context.Videos == null)
            {
                return NotFound("No videos found");
            }

            var videos = _context.Videos;

            return Ok(videos);
        }

        [HttpPost(Name = "EditVideo")]
        public ActionResult<Video> EditVideo(Video v)
        {
            if(_context.Videos == null)
            {
                return NotFound("No videos found");
            }

            var videos = _context.Videos;

            return Ok(videos);
        }
}
