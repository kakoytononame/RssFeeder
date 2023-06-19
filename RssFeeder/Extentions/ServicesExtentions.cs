using Microsoft.EntityFrameworkCore;
using RssFeeder.Contexts;
using RssFeeder.Services;

namespace RssFeeder.Extentions;

public static class ServicesExtentions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IJWTService,JWTService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IRssFeaderService, RssFeaderService>();
        return services;
    }

    public static IServiceCollection AddIdentityContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DB_IDENTITY_CONNECTION_STRING"] is null
            ? configuration.GetConnectionString("LocalDbIdentity")
            : configuration["DB_IDENTITY_CONNECTION_STRING"]!;

        services.AddDbContext<DBContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                provider => provider.EnableRetryOnFailure()
            );

        });
        return services;
    }
    
    public static IServiceCollection AddRazorServices(this IServiceCollection services)
    {
        services.AddRazorPages();
        return services;
    }
}