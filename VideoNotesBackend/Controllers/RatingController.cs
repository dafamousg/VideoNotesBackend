using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Controllers
{
    [Route("api/Rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly VideoNotesContext _context;

        public RatingController(VideoNotesContext context)
        {
            _context = context;
        }

        [HttpGet(RouteNames.GetAll)]
        public ActionResult<Rating> Get()
        {
            if(_context.Ratings == null)
            {
                return NotFound("No ratings found");
            }

            var ratings = _context.Ratings;

            return Ok(ratings);
        }
        
        [HttpPost(RouteNames.Create)]
        public async Task<ActionResult<Rating>> Create(Rating rating)
        {
            if(rating == null)
            {
                return BadRequest("Rating is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Add(rating);
            await _context.SaveChangesAsync();

            return Ok(rating);
        }
    }
}
