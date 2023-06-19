using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using RssFeeder.Contexts;
using RssFeeder.Models;

namespace RssFeeder.Services;

public class RssFeaderService:IRssFeaderService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    private readonly DBContext _dbContext;

    public RssFeaderService(IHttpClientFactory httpClientFactory,DBContext dbContext)
    {
        _httpClientFactory = httpClientFactory;
        _dbContext = dbContext;
    }

    public async Task<List<int>> AddNews(string URL)
    {
       using var httpClient = _httpClientFactory.CreateClient();

       var response=await httpClient.GetAsync(URL);

       var result=await response.Content.ReadAsStreamAsync();

       var deserializedNew = new XmlSerializer(typeof(RssFeedDes)).Deserialize(result) as RssFeedDes;

       var RssFeedsForDb = deserializedNew.ChannelDes.Items;

       List<RssItem> RSSFeeds = new List<RssItem>();

       foreach (var item in RssFeedsForDb)
       {
           if (!_dbContext.RssFeeds.Any(p => p.Title == item.Title))
           {
               RSSFeeds.Add(new RssItem
                   {
                       DbAdded = DateTime.Now,
                       Description = item.Description,
                       ItemId = Guid.NewGuid(),
                       Link = item.Link,
                       PubDate = item.PubDate,
                       Title = item.Title
                   }
               );
           }
       }
       
       await _dbContext.RssFeeds.AddRangeAsync(RSSFeeds);
       
       var count= await _dbContext.SaveChangesAsync();
       
       
       return new List<int>
       {
           count,
           RssFeedsForDb.Count-count
       };;
    }

    public async Task Read(ReadNews news)
    {
        _dbContext.Readed.Add(new IdentityUserRssItem
        {
            ItemId = news.RssItemId,
            UserId = news.UserId
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<RssItem>> GetNews(int number)
    {
       var news=await _dbContext.RssFeeds.Skip(number).Take(10).ToListAsync();
       if (news is null)
       {
           news = await _dbContext.RssFeeds.ToListAsync();
       }

       return news;
    }

}