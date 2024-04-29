using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Data
{
    public class VideoNotesContext : DbContext
    {
        public VideoNotesContext(DbContextOptions<VideoNotesContext> options)
            : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
