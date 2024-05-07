using VideoNotesBackend.ModelDto.Tag;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.ModelDto
{
    public class NoteDto: IEntity
    {
        public Guid Id { get; set; }

        public Guid? VideoId { get; set; }
        public required string Title { get; set; }
        public string? FreeText { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? Edited { get; set; }

        public List<TagDto>? Tags { get; set; }

    }
}
