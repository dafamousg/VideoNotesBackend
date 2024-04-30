using VideoNotesBackend.ModelDto.Tag;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.ModelDto
{
    public class NoteCreate
    {
        public Guid? VideoId { get; set; }
        public string? Title { get; set; }
        public string? FreeText { get; set; }

        public List<TagDto>? Tags { get; set; }

    }
}
