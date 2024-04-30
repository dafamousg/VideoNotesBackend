using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.ModelDto;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Controllers
{
    [Route("api/Note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly VideoNotesContext _context;
        public NoteController(VideoNotesContext context)
        {
            _context = context;
        }

        [HttpGet(RouteNames.GetAll)]
        public ActionResult<List<NoteDto>> Get()
        {

            if (_context.Notes == null)
            {
                return NotFound("Notes not found");
            }

            var notes = _context.Notes.Include(n => n.Tags).ToList();

            TinyMapper.Bind<List<Note>, List<NoteDto>>();
            var noteNotes = TinyMapper.Map<List<NoteDto>>(notes);

            return Ok(noteNotes);
        }

        [HttpPost(RouteNames.Create)]
        public async Task<ActionResult<NoteDto>> Create(NoteCreate createdNote)
        {

            if (createdNote == null)
            {
                return NotFound("Note object missing");
            }

            TinyMapper.Bind<NoteCreate, Note>();
            var convertedNote = TinyMapper.Map<Note>(createdNote);

            if (convertedNote.Tags.Count > 0)
            {
                for (var i = 0; i < convertedNote.Tags.Count; i++)
                {
                    var tag = convertedNote.Tags[i];

                    if (_context.Tags.Any(t => t.Name.Equals(tag.Name)))
                    {
                        var dbTag = _context.Tags.FirstOrDefault(t => t.Name.Equals(tag.Name));

                        if (dbTag != null)
                        {
                            convertedNote.Tags.Remove(tag);
                            convertedNote.Tags.Add(dbTag);
                        }
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Add(convertedNote);
            await _context.SaveChangesAsync();

            TinyMapper.Bind<Note, NoteDto>();
            var returnNote = TinyMapper.Map<NoteDto>(convertedNote);

            return Ok(returnNote);
        }

        [HttpPost(RouteNames.Update)]
        public async Task<ActionResult<NoteDto>> Update(Guid? id, [FromBody] NoteDto editedNote)
        {
            if (id == null)
            {
                return NotFound("Id is null");
            }

            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound("Note not found");
            }

            editedNote.Edited = DateTime.UtcNow;

            // Compare and Update the video
            UpdateNoteProps(note, editedNote);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound("Video could not be saved");
            }

            TinyMapper.Bind<Note, NoteDto>();
            var returnNote = TinyMapper.Map<NoteDto>(note);

            return Ok(returnNote);
        }

        [HttpDelete(RouteNames.Delete)]
        public async Task<ActionResult<string>> Delete(Guid? id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound($"Could not find Note with ID: {id}");
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok("Note deleted successfully");
        }

        private static void UpdateNoteProps(Note note, NoteDto editedNote)
        {
            TinyMapper.Bind<NoteDto, Note>();
            var newNote = TinyMapper.Map<Note>(editedNote);

            var videoProperties = typeof(Note).GetProperties()
                .Where(p => p.Name != nameof(Note.Id) && p.Name != nameof(Note.CreatedDate));

            foreach (var property in videoProperties)
            {
                var newValue = property.GetValue(newNote);
                var currentValue = property.GetValue(note);

                if (newValue != null && !newValue.Equals(currentValue))
                {
                    property.SetValue(note, newValue);
                }
            }
        }
    }
}
