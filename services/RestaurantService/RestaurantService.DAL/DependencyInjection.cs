using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantService.DAL.Interceptors;


namespace RestaurantService.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration);
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
}
