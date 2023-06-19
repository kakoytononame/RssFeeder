using System.Xml.Serialization;

namespace RssFeeder.Models;

[XmlRoot("item")]
public class RssItemDes
{
    [XmlElement("title")]
    public string Title { get; set; }

    [XmlElement("link")]
    public string Link { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }

    [XmlElement("pubDate")]
    public string PubDate { get; set; }

    [XmlElement("dc:creator")]
    public string Creator { get; set; }

    [XmlElement("category")]
    public string[] Categories { get; set; }
}   