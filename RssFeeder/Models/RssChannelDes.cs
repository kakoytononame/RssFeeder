using System.Xml.Serialization;

namespace RssFeeder.Models;

public class RssChannelDes
{
    [XmlElement("title")] public string Title { get; set; }

    [XmlElement("link")] public string Link { get; set; }

    // Добавьте другие свойства из XML-документа, которые вам нужны
    // Например:
    [XmlElement("description")] public string Description { get; set; }

    [XmlElement("item")] public List<RssItemDes> Items { get; set; }
}
