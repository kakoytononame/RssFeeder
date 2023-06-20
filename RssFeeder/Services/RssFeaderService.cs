using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using RssFeeder.Contexts;
using RssFeeder.DTOS;
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

    public async Task<List<RssItemWithFilters>> GetNews(Guid UserId)
    {
       var news=await _dbContext.RssFeeds.ToListAsync();
       var readedEntityIds=await _dbContext.Readed.Where(p => p.UserId == UserId).ToListAsync();
       var readedItemIds = new List<Guid>();
       foreach (var entity in readedEntityIds)
       {
           readedItemIds.Add(entity.ItemId);
       }
       var readedItems = await _dbContext.RssFeeds.Where(e => readedItemIds.Contains(e.ItemId)).ToListAsync();
       var newsWithFiler = new List<RssItemWithFilters>();
       for (int i = 0; i < readedItems.Count; i++)
       {
           if (news.Contains(readedItems[i]))
           {
               newsWithFiler.Add(new RssItemWithFilters
               {
                   DbAdded = readedItems[i].DbAdded,
                   Description = readedItems[i].Description,
                   ItemId = readedItems[i].ItemId,
                   Link = readedItems[i].Link,
                   PubDate = readedItems[i].PubDate,
                   ReadedFilter = "readed",
                   Title = readedItems[i].Title
                   
               });
           }
       }

       for (int i = 0; i < news.Count; i++)
       {
           if (newsWithFiler.FirstOrDefault(p=>p.ItemId==news[i].ItemId) is null)
           {
               newsWithFiler.Add(new RssItemWithFilters
               {
                   DbAdded = news[i].DbAdded,
                   Description = news[i].Description,
                   ItemId = news[i].ItemId,
                   Link = news[i].Link,
                   PubDate = news[i].PubDate,
                   ReadedFilter = "unreaded",
                   Title = news[i].Title
                   
               });
           }
           
       }
       return newsWithFiler;
    }

    public async Task<List<RssItem>> Readed(Guid UserId)
    {
       var readedEntityIds=await _dbContext.Readed.Where(p => p.UserId == UserId).ToListAsync();
       var readedItemIds = new List<Guid>();
       foreach (var entity in readedEntityIds)
       {
           readedItemIds.Add(entity.ItemId);
       }
       var readedItems = await _dbContext.RssFeeds.Where(e => readedItemIds.Contains(e.ItemId)).ToListAsync();
       foreach (var item in readedItems)
       {
           item.UsersRssItems = null;
       }
       return readedItems;
    }
    
    public async Task<List<RssItem>> UnReaded(Guid UserId)
    {
        var readedEntityIds=await _dbContext.Readed.Where(p => p.UserId == UserId).ToListAsync();
        var readedItemIds = new List<Guid>();
        foreach (var entity in readedEntityIds)
        {
            readedItemIds.Add(entity.ItemId);
        }
        var readedItems = _dbContext.RssFeeds.Where(e => readedItemIds.Contains(e.ItemId));
        var unreadedItems = await _dbContext.RssFeeds.Except(readedItems).ToListAsync();
        foreach (var item in unreadedItems)
        {
            item.UsersRssItems = null;
        }
        return unreadedItems;
    }

}