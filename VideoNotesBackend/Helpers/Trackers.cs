using Microsoft.EntityFrameworkCore;
using VideoNotesBackend.Data;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.Helpers
{
    public class Trackers
    {
        public static async Task<ICollection<T>> AssociateChildEntitiesAsync<T>(ICollection<T>? childEntities, VideoNotesContext _context) where T : class, IEntity
        {
            ArgumentNullException.ThrowIfNull(childEntities);

            //find and track child entities for update
            if (childEntities.Count != 0)
            {
                var idList = childEntities.Select(x => x.Id).ToList();

                var trackedEntityList = await _context.Set<T>().Where(e => idList.Contains(e.Id)).ToListAsync();

                childEntities.Clear();

                foreach (var entity in trackedEntityList)
                {
                    childEntities.Add(entity);
                }

                return childEntities;
            }

            return childEntities;
        }

        public static async Task<T> AssociateChildEntityAsync<T>(T? childEntity, VideoNotesContext _context) where T : class, IEntity
        {
            ArgumentNullException.ThrowIfNull(childEntity);

            var trackedEntity = await _context.Set<T>().FindAsync(childEntity.Id);

            if (trackedEntity != null)
            {
                return trackedEntity;
            }

            throw new Exception("Existing entity not found.");
        }
    }
}
