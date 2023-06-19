using System.Xml.Serialization;

namespace RssFeeder.Models;

[XmlRoot("rss")]
public class RssFeedDes
{
    [XmlElement("channel")]
    public RssChannelDes ChannelDes { get; set; }
}


