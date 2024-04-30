using System.ComponentModel.DataAnnotations;

namespace VideoNotesBackend.Models
{
    public class Video
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate {  get; set; }
        public bool Watched { get; set; }
        public TimeSpan Duration { get; set; }
        
        public int? RatingId { get; set; }

        [Url(ErrorMessage = "URL is not a valid URL")]
        public string? URL { get; set; }

        public List<Tag>? Tags { get; set; }

        //public List<Note>? Notes { get; set; }
    }
}
