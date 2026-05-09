using System.Reflection;
using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RestaurantService.API.Constants;
using RestaurantService.API.Filters;
using RestaurantService.API.Middleware;
using RestaurantService.API.Services;
using RestaurantService.BLL.Interfaces;
using MassTransit;

namespace RestaurantService.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationActionFilter>();
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
                NameClaimType = ClaimTypes.NameIdentifier,
                RoleClaimType = CustomClaims.Roles
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.AdminOnly, p => 
                p.RequireClaim(CustomClaims.Roles, "Admin"));

            options.AddPolicy(Policies.RestaurantManager, policy =>
            {
                policy.RequireClaim(CustomClaims.Roles, "RestaurantManager");
                policy.RequireClaim(CustomClaims.IsVerified, "true");
            });
            
            options.AddPolicy(Policies.ManagerOrAdmin, policy => 
            {
                policy.RequireAssertion(context =>
                    context.User.HasClaim(CustomClaims.Roles, "Admin") ||
                    (context.User.HasClaim(CustomClaims.Roles, "RestaurantManager") && 
                     context.User.HasClaim(CustomClaims.IsVerified, "true")));
            });
            
            options.AddPolicy("UnverifiedManagerOrAdmin", policy => 
            {
                policy.RequireAssertion(context =>
                    context.User.HasClaim(CustomClaims.Roles, "Admin") ||
                    context.User.HasClaim(CustomClaims.Roles, "RestaurantManager"));
            });
        });
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitConfig = configuration.GetSection("RabbitMQ");
                
                cfg.Host(rabbitConfig["Host"] ?? "localhost", "/", h =>
                {
                    h.Username(rabbitConfig["Username"] ?? "guest");
                    h.Password(rabbitConfig["Password"] ?? "guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddOpenApi();

        return services;
    }
}
