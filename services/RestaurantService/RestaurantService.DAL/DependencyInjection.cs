using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.BLL.Services.Interfaces;
using RestaurantService.DAL.Interceptors;
using RestaurantService.DAL.Persistence.Repositories;
using RestaurantService.DAL.Redis;
using RestaurantService.DAL.Repositories;
using StackExchange.Redis;

namespace RestaurantService.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration)
            .AddRepositories()
            .AddRedis(configuration);
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<IRestaurantDocumentRepository, RestaurantDocumentRepository>();

        return services;
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
