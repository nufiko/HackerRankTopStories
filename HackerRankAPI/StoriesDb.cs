using HackerRankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HackerRankAPI
{
    public class StoriesDbContext : DbContext
    {
        public StoriesDbContext(DbContextOptions<StoriesDbContext> options) : base(options)
        {
        }

        public DbSet<CachedStory> Stories => Set<CachedStory>();
    }
}
