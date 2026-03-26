using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantService.BLL.Services.Interfaces;
using RestaurantService.DAL.Interceptors;
using RestaurantService.DAL.Redis;
using StackExchange.Redis;

namespace RestaurantService.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration)
            .AddRedis(configuration);
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<EntityInterceptor>();
        services.AddDbContext<RestaurantDbContext>((serviceProvider, options) =>
        {
            var interceptor = serviceProvider.GetRequiredService<EntityInterceptor>();

            options.UseNpgsql(connectionString)
                   .AddInterceptors(interceptor);
        });

        return services;
    }

    private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis");

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConnectionString));
        services.AddScoped<IGeoService, RedisGeoService>();

        return services;
    }
}
