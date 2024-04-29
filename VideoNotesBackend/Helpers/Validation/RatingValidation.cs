using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Helpers.Validation
{
    public class RatingValidation
    {
        public static bool IsValidRating(DbSet<Rating> dbRating, int ratingId)
        {
            return dbRating.Any(r => r.Id == ratingId);
        }
    }

}
