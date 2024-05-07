using System.ComponentModel.DataAnnotations;

namespace VideoNotesBackend.Models
{
    public class Tag: IEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = new Guid();
        [Required]
        public required string Name { get; set; }
        public List<Note>? Notes { get; set; }
        public List<Video>? Videos { get; set; }

    }
}
