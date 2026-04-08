using RestaurantService.BLL.Mappers.Interfaces;
using System.Reflection;

namespace RestaurantService.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationMappers(this IServiceCollection services)
    {
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
}
