using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Data;
using VideoNotesBackend.Models;

namespace MvcICT.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new VideoNotesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<VideoNotesContext>>());

            Rating r1 = new() { Name = "Not Informative" },
                r2 = new() { Name = "Partially Informative" },
                r3 = new() { Name = "Highly Informative" };

            if (!context.Ratings.Any())
            {
                context.Ratings.AddRange(r1,r2,r3);
            }

            if (!context.Videos.Any())
            {
                context.Videos.AddRange(
                    new Video
                    {
                        Title = "Ends Vol. 1",
                        ReleaseDate = DateTime.Parse("2022-06-20"),
                        Watched = false,
                        Rating = r1,
                        URL = "https://www.youtube.com/watch?v=ASxSiOi6nJI&list=PLVgHx4Z63pabcsTrvY5XsVdsvTN2ho6pz&index=2"
                    },
                    new Video
                    {
                        Title = "Ends Vol. 2",
                        ReleaseDate = DateTime.Parse("2022-08-06"),
                        Watched = false,
                        Rating = r3,
                        URL = "https://www.youtube.com/watch?v=Tex87Lo2HdE&list=PLVgHx4Z63pabcsTrvY5XsVdsvTN2ho6pz&index=2"
                    }
                );
            }

            if (!context.Notes.Any())
            {
                context.Notes.AddRange(
                    new Note
                    {
                        Title = "Note for Ends 1",
                        CreatedDate = DateTime.UtcNow
                    },
                    new Note
                    {
                        Title = "Note for Ends 2",
                        CreatedDate = DateTime.UtcNow.AddDays(-5)
                    }
                );
            }

            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Tag { Name = "PDA" },
                    new Tag { Name = "Quarter Theory" },
                    new Tag { Name = "Mindset" }
                );
            }

            context.SaveChanges();
        }
    }
}
