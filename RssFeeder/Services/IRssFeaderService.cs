using RssFeeder.Models;

namespace RssFeeder.Services;

public interface IRssFeaderService
{
    public Task<List<int>> AddNews(string URL);

    public Task Read(ReadNews news);

    public Task<List<RssItem>> GetNews(int number);
}