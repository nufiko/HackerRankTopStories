using HackerRankAPI.Models;

namespace HackerRankAPI.Interfaces
{
    public interface IStoriesCache
    {
        void AddStories(IEnumerable<Story> story);

        Task<Story?> GetStory(int id);

        Task<IEnumerable<Story>> GetStories();
    }
}
