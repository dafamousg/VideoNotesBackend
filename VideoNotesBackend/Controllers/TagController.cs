using Microsoft.AspNetCore.Mvc;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Controllers
{
    [Route("api/Tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private VideoNotesContext _context;
        public TagController(VideoNotesContext context)
        {
            _context = context;
        }

        [HttpGet(RouteNames.GetAll)]
        public ActionResult<Tag> GetAll()
        {
            if (_context.Tags == null)
            {
                return NotFound("Tags not found");
            }

            var tags = _context.Tags;

            return Ok(tags);
        }

        [HttpPost(RouteNames.Create)]
        public async Task<ActionResult<Note>> Create(Tag newTag)
        {

            if (newTag == null)
            {
                return NotFound("Tag object missing");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Add(newTag);
            await _context.SaveChangesAsync();

            return Ok(newTag);
        }

        [HttpDelete(RouteNames.Delete)]
        public async Task<ActionResult<Tag>> Delete(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if(tag == null)
            {
                return NotFound($"Could not find tag with ID: {id}");
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return Ok("Tag deleted successfully");
        }
    }
}
