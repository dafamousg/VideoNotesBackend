using System.ComponentModel.DataAnnotations;

namespace VideoNotesBackend.ModelDto.Tag
{
    public class TagDto
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
    }
}
