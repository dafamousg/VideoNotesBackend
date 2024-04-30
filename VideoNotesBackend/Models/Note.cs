using System.ComponentModel.DataAnnotations;

namespace VideoNotesBackend.Models
{
    public class Note
    {
        public Guid Id { get; set; }

        public Guid? VideoId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }
        public string? FreeText { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? Edited { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
