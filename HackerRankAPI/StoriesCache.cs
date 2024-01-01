using HackerRankAPI.Extensions;
using HackerRankAPI.Interfaces;
using HackerRankAPI.Models;
using System.Data;

namespace HackerRankAPI
{
    public class StoriesCache : IStoriesCache
    {
        private const int cacheValidSeconds = 60;
        private StoriesDbContext _context;

        public StoriesCache(StoriesDbContext dbContext) 
        {
            _context = dbContext;
        }

        public void AddStories(IEnumerable<Story> story)
        {
            _context.Stories.AddRange(story.Select(s => s.ToCachedStory()));
            _context.SaveChanges();
        }

        public async Task<Story?> GetStory(int id)
        {
            await ClearExpiredStories();
            var cachedValidStory = _context.Stories.FirstOrDefault(s => s.Id == id);
         
            return cachedValidStory;
        }

        public async Task<IEnumerable<Story>> GetStories()
        {
            await ClearExpiredStories();
            var cachedStories = _context.Stories;

            return cachedStories;
                
        }

        private async Task ClearExpiredStories()
        {
            var expiredStories = _context.Stories.Where(s => s.CachedTime.AddSeconds(cacheValidSeconds) < DateTime.UtcNow);
            _context.Stories.RemoveRange(expiredStories);

            await _context.SaveChangesAsync();
        }
    }
}
