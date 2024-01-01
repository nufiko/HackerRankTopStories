using HackerRankAPI.Models;

namespace HackerRankAPI.Interfaces
{
    public interface IStoriesProvider
    {
        IEnumerable<Story> GetNTopStories(int number);
    }
}
