using FluentValidation;
using RestaurantService.API.Filters;
using RestaurantService.API.Middleware;
using System.Reflection;

namespace RestaurantService.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationActionFilter>();
        });

        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddOpenApi();

        return services;
    }
}
