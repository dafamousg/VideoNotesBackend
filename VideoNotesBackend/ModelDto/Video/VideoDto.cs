using System.ComponentModel.DataAnnotations;
using VideoNotesBackend.ModelDto.Tag;

namespace VideoNotesBackend.ModelDto.Video
{
    public class VideoDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public bool Watched { get; set; }
        public TimeSpan Duration { get; set; }
        public int RatingId { get; set; }

        [Url]
        public string? URL { get; set; }
        public List<TagDto>? Tags { get; set; }

        //public ICollection<Note>? Notes { get; set; }
    }
}
