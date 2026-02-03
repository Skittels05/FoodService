using AuthService.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMappings()
            .AddMediator();
    }

    private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
            cfg.AddMaps(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(PagedListConverter<,>));
        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
