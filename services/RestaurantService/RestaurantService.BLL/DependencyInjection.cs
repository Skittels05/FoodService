using Microsoft.Extensions.DependencyInjection;
using RestaurantService.BLL.Mappers;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Services;
using RestaurantService.BLL.Services.Interfaces;
using System.Reflection;

namespace RestaurantService.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        return services
            .AddMappers()
            .AddServices();
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IMappingService, MappingService>();

        var mapperTypePairs = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(
                t => t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)),
                (impl, iface) => (impl, iface)
            );

        foreach (var pair in mapperTypePairs)
        {
            services.AddScoped(pair.iface, pair.impl);
        }

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IRestaurantDocumentService, RestaurantDocumentService>();
        services.AddScoped<IRestaurantService, Services.RestaurantService>();
        services.AddScoped<IStopListService, StopListService>();

        return services;
    }
}
