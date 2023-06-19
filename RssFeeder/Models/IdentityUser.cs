using System.ComponentModel.DataAnnotations;

namespace RssFeeder.Models;

public class IdentityUser
{
    [Key]
    public Guid UserId { get; set; }
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Role { get; set; }
    
    public ICollection<IdentityUserRssItem> UsersRssItems { get; set; }
}