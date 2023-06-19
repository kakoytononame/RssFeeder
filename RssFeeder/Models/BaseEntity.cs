using System.ComponentModel.DataAnnotations;

namespace RssFeeder.Models;

public class BaseEntity<T>
{
    [Key]
    public T? Id { get; set; }
}