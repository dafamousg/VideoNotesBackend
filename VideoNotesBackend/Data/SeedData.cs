using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Data;
using VideoNotesBackend.Enums;
using VideoNotesBackend.Models;

namespace MvcICT.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new VideoNotesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<VideoNotesContext>>())) 
            {
                if(context.Videos.Any())
                {
                    return;
                }

                context.Videos.AddRange(
                    new Video
                    {
                        Title = "Ends Vol. 1",
                        ReleaseDate = DateTime.Parse("2022-06-20"),
                        Watched = false,
                        Rating = Rating.Values.HighlyInformative,
                        URL = "https://www.youtube.com/watch?v=ASxSiOi6nJI&list=PLVgHx4Z63pabcsTrvY5XsVdsvTN2ho6pz&index=2"
                    },
                    new Video
                    {
                        Title = "Ends Vol. 2",
                        ReleaseDate = DateTime.Parse("2022-08-06"),
                        Watched = false,
                        Rating = Rating.Values.PartiallyInformative,
                        URL = "https://www.youtube.com/watch?v=Tex87Lo2HdE&list=PLVgHx4Z63pabcsTrvY5XsVdsvTN2ho6pz&index=2"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
