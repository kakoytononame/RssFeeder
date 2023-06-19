using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RssFeeder.Models;

namespace RssFeeder.Configs;

public class RssConfiguration:IEntityTypeConfiguration<RssItem>
{
    public void Configure(EntityTypeBuilder<RssItem> builder)
    {
        builder.HasIndex(p => p.ItemId).IsUnique();
        builder.HasIndex(p => p.Title).IsUnique();
    }
}