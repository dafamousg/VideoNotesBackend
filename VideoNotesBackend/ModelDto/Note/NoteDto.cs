using VideoNotesBackend.ModelDto.Tag;

namespace VideoNotesBackend.ModelDto
{
    public class NoteDto
    {
        public Guid Id { get; set; }

        public Guid? VideoId { get; set; }
        public string? Title { get; set; }
        public string? FreeText { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? Edited { get; set; }

        public List<TagDto>? Tags { get; set; }

    }
}
