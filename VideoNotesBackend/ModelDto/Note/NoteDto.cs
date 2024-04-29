using VideoNotesBackend.Models;

namespace VideoNotesBackend.ModelDto
{
    public class NoteDto
    {
        public Guid Id { get; set; }

        // Not sure of correct
        public Guid? VideoId { get; set; }
        public string? Title { get; set; }
        public string? FreeText { get; set; }

        public DateTime? Edited { get; set; }

        public List<Tag>? Tags { get; set; }

    }
}
