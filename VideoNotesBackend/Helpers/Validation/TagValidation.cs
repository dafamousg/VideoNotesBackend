using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Helpers.Validation
{
    public class TagValidation
    {
        public static bool NameExists(DbSet<Tag> tags, Tag tag)
        {
            return tags.Any(t => t.Name.Equals(tag.Name));
        }
    }
}
