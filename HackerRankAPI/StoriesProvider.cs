using HackerRankAPI.Extensions;
using HackerRankAPI.Interfaces;
using HackerRankAPI.JsonConverters;
using HackerRankAPI.Models;
using System.Text.Json;

namespace HackerRankAPI
{
    public class StoriesProvider : IStoriesProvider
    {
        private IStoriesCache _cache;
        private IHttpClientFactory _httpClientFactory;
        private JsonSerializerOptions jsonOptions; 

        public StoriesProvider(IStoriesCache cache, IHttpClientFactory httpClientFactory)
        {
            _cache = cache;
            _httpClientFactory = httpClientFactory;
            jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            jsonOptions.Converters.Add(new UnixEpochDateTimeConverter());
        }

        public IEnumerable<Story> GetNTopStories(int number)
        {
            var storiesIdsTask = GetTopStoriesIds();
            var cachedStoriesTask = _cache.GetStories();

            Task.WaitAll(storiesIdsTask, cachedStoriesTask);
            var storiesIds = storiesIdsTask.Result.ToList();
            var cachedStories = cachedStoriesTask.Result.ToList();
            var missingStoriesInCache = storiesIds.Where(id => !cachedStories.Select(s => s.Id).Contains(id));

            var storiesTask = missingStoriesInCache.Select(GetStory);

            Task.WaitAll(storiesTask.ToArray());

            var newStories = storiesTask.Select(t => t.Result.ToStory()).ToList();

            _cache.AddStories(newStories);

            cachedStories.AddRange(newStories);

            return cachedStories.OrderByDescending(s => s.Score).Take(number);
        }

        private async Task<IEnumerable<int>> GetTopStoriesIds()
        {
            var requestUri = @"https://hacker-news.firebaseio.com/v0/beststories.json";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(requestUri);

            if(response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return Enumerable.Empty<int>();
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<int[]>(result, options: jsonOptions);
        }

        private async Task<HackerRankStory?> GetStory(int id)
        {
            var requestUri = @$"https://hacker-news.firebaseio.com/v0/item/{id}.json";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(requestUri);

            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<HackerRankStory>(result, options: jsonOptions);
        }

    }
}
