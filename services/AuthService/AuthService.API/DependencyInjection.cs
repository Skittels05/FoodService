using AuthService.API.Infrastructure;
using Serilog;

namespace AuthService.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddControllers();
        services.AddOpenApi();
        return services;
    }

    public static WebApplicationBuilder AddApiLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());
        return builder;
    }
}
