using System.ComponentModel.DataAnnotations;

namespace VideoNotesBackend.Models
{
    public class Rating
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public required string Name { get; set; }
    }
}
