using Microsoft.AspNetCore.Http;
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
        public ActionResult<Note> Get()
        {

            if (_context.Notes == null)
            {
                return NotFound("Notes not found");
            }

            var notes = _context.Notes;

            return Ok(notes);
        }

        [HttpPost(RouteNames.Create)]
        public async Task<ActionResult<Note>> Create(Note newNote)
        {

            if (newNote == null)
            {
                return NotFound("Note object missing");
            }

            newNote.CreatedDate = DateTime.UtcNow;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Add(newNote);
            await _context.SaveChangesAsync();

            return Ok(newNote);
        }

        [HttpPost(RouteNames.Update)]
        public async Task<ActionResult<Video>> Update(Guid? id, [FromBody] NoteDto editedNote)
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

            return Ok(note);
        }

        [HttpDelete(RouteNames.Delete)]
        public async Task<ActionResult<Note>> Delete(Guid? id)
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
