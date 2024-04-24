using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoNotesBackend.Data;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Controllers
{
    [Route("api/rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly VideoNotesContext _context;

        public RatingController(VideoNotesContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public ActionResult<Rating> GetAll()
        {
            if(_context.Ratings == null)
            {
                return NotFound("No ratings found");
            }

            var ratings = _context.Ratings;

            return Ok(ratings);
        }
    }
}
