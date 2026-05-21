using AuthService.API.Constants;
using AuthService.API.Infrastructure;
using AuthService.API.Services;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using AuthService.API.Consumers;
using MassTransit;

namespace AuthService.API;

public static class DependencyInjection
{
    public const string FrontendCorsPolicy = "FrontendCorsPolicy";
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddControllers();
        services.AddOpenApi();
        
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
        
        services.AddCors(options =>
        {
            options.AddPolicy(FrontendCorsPolicy, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = configuration["Auth0:Domain"];
            options.Audience = configuration["Auth0:Audience"];

            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.AdminOnly, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("https://food-service.com/roles", "Admin");
            });
            options.AddPolicy(Policies.CourierOrAdmin, policy =>
            {
                policy.RequireAuthenticatedUser();

                policy.RequireAssertion(context =>
                {
                    var isAdmin = context.User.HasClaim("https://food-service.com/roles", "Admin");
                    var isVerifiedCourier = context.User.HasClaim("https://food-service.com/roles", "Courier") &&
                                            context.User.HasClaim("https://food-service.com/is_verified", "true");
                    return isAdmin || isVerifiedCourier;
                });
            });
            options.AddPolicy(Policies.CustomerOrAdmin, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context =>
                    context.User.HasClaim("https://food-service.com/roles", "Admin") ||
                    context.User.HasClaim("https://food-service.com/roles", "Customer")
                );
            });
            options.AddPolicy(Policies.RestaurantManagerOrAdmin, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context =>
                    context.User.HasClaim("https://food-service.com/roles", "Admin") ||
                    context.User.HasClaim("https://food-service.com/roles", "RestaurantManager")
                );
            });
        });
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<RestaurantVerifiedConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitConfig = configuration.GetSection("RabbitMQ");
                
                cfg.Host(rabbitConfig["Host"] ?? "localhost", "/", h =>
                {
                    h.Username(rabbitConfig["Username"]);
                    h.Password(rabbitConfig["Password"]);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

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
