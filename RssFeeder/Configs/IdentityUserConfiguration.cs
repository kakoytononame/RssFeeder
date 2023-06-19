using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RssFeeder.Models;

namespace RssFeeder.Configs;

public class IdentityUserConfiguration:IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.HasIndex(p => p.UserId).IsUnique();

        builder.HasData(
            new IdentityUser
            {
                UserId = Guid.Parse("921eac24-b306-4c5e-95b9-56b71feea6ae"),
                Login = "Admin",
                Password = "Admin",
                Role = "admin"
            },
            new IdentityUser
            {
                UserId = Guid.Parse("6e1b8139-e6f2-47ae-8fed-401b6e46b192"),
                Login = "User",
                Password = "User",
                Role = "user"
            }

        );
    }
    
    
}