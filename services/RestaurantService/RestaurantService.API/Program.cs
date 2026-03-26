using RestaurantService.BLL;
using RestaurantService.DAL;

namespace RestaurantService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDataAccessLayer(builder.Configuration);
        builder.Services.AddBusinessLogicLayer();

        var app = builder.Build();

        app.Run();
    }
}
