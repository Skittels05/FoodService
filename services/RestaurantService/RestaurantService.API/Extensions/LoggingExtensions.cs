using Serilog;
using Serilog.Exceptions;

namespace RestaurantService.API.Extensions;

public static class LoggingExtensions
{
    public static WebApplicationBuilder AddApiLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.WithExceptionDetails());

        return builder;
    }

    public static WebApplication UseApiLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = (httpContext, elapsed, ex) =>
            {
                if (httpContext.Request.Path.StartsWithSegments("/openapi") || 
                    httpContext.Request.Path.StartsWithSegments("/scalar"))
                {
                    return Serilog.Events.LogEventLevel.Debug;
                }
                return ex != null || httpContext.Response.StatusCode > 499 
                    ? Serilog.Events.LogEventLevel.Error 
                    : Serilog.Events.LogEventLevel.Information;
            };
        });

        return app;
    }
}
