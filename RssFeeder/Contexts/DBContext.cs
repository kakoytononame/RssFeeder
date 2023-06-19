using Microsoft.EntityFrameworkCore;
using RssFeeder.Configs;
using RssFeeder.Models;

namespace RssFeeder.Contexts;

public class DBContext:DbContext
{
    public DbSet<IdentityUser> IdentityUsers => Set<IdentityUser>();

    public DbSet<RssItem> RssFeeds => Set<RssItem>();

    public DbSet<IdentityUserRssItem> Readed => Set<IdentityUserRssItem>();

    public DBContext(DbContextOptions<DBContext> options):base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new IdentityUserConfiguration());
        modelBuilder.ApplyConfiguration(new RssConfiguration());
        modelBuilder.ApplyConfiguration(new IdentityUsersRssFeedersConfiguration());
        modelBuilder.Entity<IdentityUserRssItem>()
            .HasKey(ur => new { ur.UserId, ur.ItemId });

        modelBuilder.Entity<IdentityUserRssItem>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UsersRssItems)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<IdentityUserRssItem>()
            .HasOne(ur => ur.RssItem)
            .WithMany(r => r.UsersRssItems)
            .HasForeignKey(ur => ur.ItemId);
    }
}