namespace RssFeeder.Models;

public class RssChannel
{
    
    public string Title { get; set; }

    public string Link { get; set; }
     
    public string Description { get; set; }

    public List<RssItemDes> Items { get; set; }
}