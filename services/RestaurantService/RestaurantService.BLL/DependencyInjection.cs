using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using RestaurantService.BLL.Mappers;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Services;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        return services
            .AddValidators()
            .AddMappers()
            .AddServices();
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IMappingService, MappingService>();

        var mapperTypePairs = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces(), (impl, iface) => new { impl, iface })
            .Where(x => x.iface.IsGenericType && x.iface.GetGenericTypeDefinition() == typeof(IMapper<,>));

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
