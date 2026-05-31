using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RestaurantService.DAL;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        
        try
        {
            var context = scopedServices.GetRequiredService<RestaurantDbContext>(); 
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = scopedServices.GetRequiredService<ILogger<RestaurantDbContext>>();
            logger.LogError(ex, "An error occurred while migrating the restaurant database.");
        }
    }
}
