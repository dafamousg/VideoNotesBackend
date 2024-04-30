using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.ModelDto.Tag;
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
        public ActionResult<List<TagDto>> GetAll()
        {
            if (_context.Tags == null)
            {
                return NotFound("Tags not found");
            }

            var tags = _context.Tags.ToList();

            TinyMapper.Bind<List<Tag>, List<TagDto>>();
            var returnTag = TinyMapper.Map<List<TagDto>>(tags);

            return Ok(returnTag);
        }

        [HttpPost(RouteNames.Create)]
        public async Task<ActionResult<TagDto>> Create(Tag newTag)
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


            TinyMapper.Bind<Tag, TagDto>();
            var returnTag = TinyMapper.Map<TagDto>(newTag);

            return Ok(returnTag);
        }

        [HttpDelete(RouteNames.Delete)]
        public async Task<ActionResult<string>> Delete(int id)
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
