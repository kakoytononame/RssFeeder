using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RssFeeder.Models;

namespace RssFeeder.Configs;

public class IdentityUsersRssFeedersConfiguration:IEntityTypeConfiguration<IdentityUserRssItem>
{
    public void Configure(EntityTypeBuilder<IdentityUserRssItem> builder)
    {
        builder.Ignore(p => p.RssItem);
        builder.Ignore(p => p.User);
        builder.HasIndex(p => p.ItemId).IsUnique();
    }
}