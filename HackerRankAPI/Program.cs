using HackerRankAPI;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using HackerRankAPI.Models;
using HackerRankAPI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddDbContext<StoriesDbContext>(o => o.UseInMemoryDatabase("CachedStories"));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IStoriesCache, StoriesCache>();
builder.Services.AddScoped<IStoriesProvider, StoriesProvider>();

var app = builder.Build();

var todosApi = app.MapGroup("/TopStories");
todosApi.MapGet("/", (int number, IStoriesProvider provider) =>
{
    var result = provider.GetNTopStories(number); 
    return result;
});

app.Run();

[JsonSerializable(typeof(IEnumerable<Story>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}

