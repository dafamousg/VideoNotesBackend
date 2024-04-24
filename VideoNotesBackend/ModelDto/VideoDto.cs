using System.ComponentModel.DataAnnotations;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.ModelDto
{
    public class VideoDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public bool Watched { get; set; }
        public TimeSpan Duration { get; set; }

        // Not sure if this is correct (Update: I think this is okay)
        public int RatingId { get; set; }

        // Will add tags later
        // Not sure if this is correct
        //public string[]? Tags { get; set; }  // Existing & new

        [Url]
        public string? URL { get; set; }

        //public ICollection<Note>? Notes { get; set; }
    }
}
