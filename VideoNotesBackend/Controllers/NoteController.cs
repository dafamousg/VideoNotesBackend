using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.Helpers.Converter;
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

            var convNote = Converter.TypeToDto<List<Note>, List<NoteDto>>(notes);

            return Ok(convNote);
        }

        [HttpPost(RouteNames.Create)]
        public async Task<ActionResult<NoteDto>> Create(NoteCreate createdNote)
        {

            if (createdNote == null)
            {
                return NotFound("Note object missing");
            }

            var convertedNote = Converter.TypeToDto<NoteCreate, Note>(createdNote);

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

            var returnNote = Converter.TypeToDto<Note, NoteDto>(convertedNote);

            return Ok(returnNote);
        }

        [HttpPost(RouteNames.Update)]
        public async Task<ActionResult<NoteDto>> Update(Guid? id, [FromBody] NoteDto editedNote)
        {
            if (id == null)
            {
                return NotFound("Id is null");
            }

            var note = await _context.Notes.Include(n => n.Tags).SingleOrDefaultAsync(n => n.Id == id);

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

            var returnNote = Converter.TypeToDto<Note, NoteDto>(note);

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
            var newNote = Converter.TypeToDto<NoteDto, Note>(editedNote);

            var noteProperties = typeof(Note).GetProperties()
                .Where(p => p.Name != nameof(Note.Id) && p.Name != nameof(Note.CreatedDate));

            foreach (var property in noteProperties)
            {
                var newValue = property.GetValue(newNote);
                var currentValue = property.GetValue(note);

                // Checks if the prop is non-value type      
                var isNullable = !property.PropertyType.IsValueType ||
                    // Checks if the property is a nullable Value type
                    Nullable.GetUnderlyingType(property.PropertyType) != null;

                bool isNullableEntry = isNullable || (!isNullable && newValue != null);

                if (isNullableEntry && !newValue.Equals(currentValue))
                {
                    property.SetValue(note, newValue);
                }
            }
        }
    }
}
