using System.ComponentModel.DataAnnotations;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.ModelDto.Tag
{
    public class TagDto: IEntity
    {
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
    }
}
