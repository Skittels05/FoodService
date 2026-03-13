using Microsoft.EntityFrameworkCore;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL;

public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : DbContext(options)
{
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<RestaurantDocument> RestaurantDocuments => Set<RestaurantDocument>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<StopListItem> StopListItems => Set<StopListItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantDbContext).Assembly);
    }
}
