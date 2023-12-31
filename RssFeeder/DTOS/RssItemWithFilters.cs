﻿namespace RssFeeder.DTOS;

public class RssItemWithFilters
{
    public Guid ItemId { get; set; }
    
    public string Title { get; set; }
    
    public string Link { get; set; }
    
    public string Description { get; set; }
    
    public string PubDate { get; set; }
    
    public DateTime DbAdded { get; set; }
    
    public string ReadedFilter { get; set; }
    
}