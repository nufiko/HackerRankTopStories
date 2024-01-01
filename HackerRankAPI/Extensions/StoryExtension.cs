using HackerRankAPI.Models;

namespace HackerRankAPI.Extensions
{
    public static class StoryExtension
    {
        public static CachedStory ToCachedStory(this Story story)
        {
            if (story == null)
            {
                return null;
            }

            var cachedStory = new CachedStory();
            cachedStory.PostedBy = story.PostedBy;
            cachedStory.Id = story.Id;
            cachedStory.Title = story.Title;
            cachedStory.Score = story.Score;
            cachedStory.CommentCount = story.CommentCount;
            cachedStory.Uri = story.Uri;
            cachedStory.Time = story.Time;
            cachedStory.CachedTime = DateTime.UtcNow;

            return cachedStory;
        }

        public static Story ToStory(this HackerRankStory hackerRankStory)
        {
            if (hackerRankStory == null)
            {
                return null;
            }

            var story = new Story();
            story.PostedBy = hackerRankStory.By;
            story.Id = hackerRankStory.Id;
            story.Title = hackerRankStory.Title;
            story.Score = hackerRankStory.Score;
            story.CommentCount = hackerRankStory.Kids.Length;
            story.Uri = hackerRankStory.Url;
            story.Time = hackerRankStory.Time;

            return story;
        }
    }
}
