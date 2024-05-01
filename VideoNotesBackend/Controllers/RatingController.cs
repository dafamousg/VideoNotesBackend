using Microsoft.AspNetCore.Mvc;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.Helpers.Converter;
using VideoNotesBackend.ModelDto.Rating;
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
        public ActionResult<List<RatingDto>> Get()
        {
            if(_context.Ratings == null)
            {
                return NotFound("No ratings found");
            }

            var ratings = _context.Ratings.ToList();

            var returnRating = Converter.TypeToDto<List<Rating>, List<RatingDto>>(ratings);

            return Ok(returnRating);
        }
        
        [HttpPost(RouteNames.Create)]
        public async Task<ActionResult<RatingDto>> Create(Rating rating)
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

            var returnRating = Converter.TypeToDto<Rating, RatingDto>(rating);

            return Ok(returnRating);
        }
    }
}
