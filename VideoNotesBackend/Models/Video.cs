using System.ComponentModel.DataAnnotations;
using VideoNotesBackend.Enums;

namespace VideoNotesBackend.Models
{
    public class Video
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate {  get; set; }
        public bool Watched { get; set; }
        public TimeSpan Duration { get; set; }
        
        // Not sure if this is correct (Update: I think this is okay)
        public Rating.Values? Rating { get; set; }

        // Will add tags later
        // Not sure if this is correct
        //public string[]? Tags { get; set; }  // Existing & new

        [Url]
        public string? URL { get; set; }

        //public ICollection<Note>? Notes { get; set; }
    }

    public class VideoEditDto
    {
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public bool Watched { get; set; }
        public TimeSpan Duration { get; set; }

        // Not sure if this is correct (Update: I think this is okay)
        public Rating.Values? Rating { get; set; }

        // Will add tags later
        // Not sure if this is correct
        //public string[]? Tags { get; set; }  // Existing & new

        [Url]
        public string? URL { get; set; }

        //public ICollection<Note>? Notes { get; set; }
    }
}
