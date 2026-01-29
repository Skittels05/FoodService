using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

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
