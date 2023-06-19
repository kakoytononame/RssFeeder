namespace RssFeeder.Models;

public class IdentityUserRssItem
{
     public Guid UserId { get; set; }
     public IdentityUser? User { get; set; }

     public Guid ItemId { get; set; }
     public RssItem? RssItem { get; set; }
}