using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Helpers.Validation
{
    public class RatingValidation : ValidationAttribute
    {
        public static bool IsValidRating(DbSet<Rating> dbRating, int ratingId)
        {
            return dbRating.Any(r => r.Id == ratingId);
        }
    }

}
