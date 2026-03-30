using AuthService.Application.Interfaces;
using AuthService.Domain.Interfaces;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Persistence.Interceptors;
using AuthService.Infrastructure.Persistence.Repositories;
using AuthService.Infrastructure.Services;
using AuthService.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace AuthService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddContext(configuration)
            .AddExternalServices(configuration)
            .AddRedis(configuration);
    }

    private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<EntityInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var auditableInterceptor = sp.GetRequiredService<EntityInterceptor>();
            options.UseNpgsql(connectionString)
                   .AddInterceptors(auditableInterceptor);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<Auth0ManagementSettings>()
            .BindConfiguration(Auth0ManagementSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddScoped<IAuth0RoleService, Auth0RoleService>();

        return services;
    }

    private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis")
            ?? throw new InvalidOperationException("Redis connection string is missing.");

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConnectionString));
        services.AddScoped<IGeoService, RedisGeoService>();

        return services;
    }
}
