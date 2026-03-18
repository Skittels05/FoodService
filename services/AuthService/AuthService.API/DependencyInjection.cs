using AuthService.API.Constants;
using AuthService.API.Infrastructure;
using AuthService.API.Services;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;

namespace AuthService.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddControllers();
        services.AddOpenApi();

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
