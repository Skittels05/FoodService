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
            options.AddPolicy(Policies.AdminOnly, p => p.RequireClaim(CustomClaims.Roles, "Admin"));
            options.AddPolicy(Policies.RestaurantManager, p => p.RequireClaim(CustomClaims.Roles, "RestaurantManager"));
            options.AddPolicy(Policies.ManagerOrAdmin, p => 
                p.RequireAssertion(c => c.User.HasClaim(CustomClaims.Roles, "Admin") 
                                     || c.User.HasClaim(CustomClaims.Roles, "RestaurantManager")));
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
