using System.ComponentModel.DataAnnotations;

namespace RssFeeder.Models;

public class RssItem
{
    [Key]
    public Guid ItemId { get; set; }
    
    public string Title { get; set; }
    
    public string Link { get; set; }
    
    public string Description { get; set; }
    
    public string PubDate { get; set; }
    public DateTime DbAdded { get; set; }
    
    public ICollection<IdentityUserRssItem> UsersRssItems { get; set; }
}