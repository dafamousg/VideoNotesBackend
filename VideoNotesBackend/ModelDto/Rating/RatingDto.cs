using VideoNotesBackend.Models;

namespace VideoNotesBackend.ModelDto.Rating
{
    public class RatingDto: IEntity
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
    }
}
