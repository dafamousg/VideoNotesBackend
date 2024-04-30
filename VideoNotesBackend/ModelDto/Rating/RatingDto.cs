using System.ComponentModel.DataAnnotations;

namespace VideoNotesBackend.ModelDto.Rating
{
    public class RatingDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
