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

            if (!context.Ratings.Any())
            {
                context.Ratings.AddRange(
                    new Rating { Name = "Not Informative" },
                    new Rating { Name = "Partially Informative" },
                    new Rating { Name = "Highly Informative" }
                );
            }

            if (!context.Videos.Any())
            {
                context.Videos.AddRange(
                    new Video
                    {
                        Title = "Ends Vol. 1",
                        ReleaseDate = DateTime.Parse("2022-06-20"),
                        Watched = false,
                        RatingId = 1,
                        URL = "https://www.youtube.com/watch?v=ASxSiOi6nJI&list=PLVgHx4Z63pabcsTrvY5XsVdsvTN2ho6pz&index=2"
                    },
                    new Video
                    {
                        Title = "Ends Vol. 2",
                        ReleaseDate = DateTime.Parse("2022-08-06"),
                        Watched = false,
                        RatingId = 3,
                        URL = "https://www.youtube.com/watch?v=Tex87Lo2HdE&list=PLVgHx4Z63pabcsTrvY5XsVdsvTN2ho6pz&index=2"
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
