using RssFeeder.DTOS;
using RssFeeder.Models;

namespace RssFeeder.Services;

public interface IRssFeaderService
{
    public Task<List<int>> AddNews(string URL);

    public Task Read(ReadNews news);

    public Task<List<RssItemWithFilters>> GetNews(Guid UserId);

    public Task<List<RssItem>> Readed(Guid UserId);

    public Task<List<RssItem>> UnReaded(Guid UserId);
}