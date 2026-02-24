using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Persistence.Interceptors;
using AuthService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddContext(configuration);
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
}
