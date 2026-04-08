using RestaurantService.BLL;
using RestaurantService.DAL;
using Scalar.AspNetCore; // <-- Подключаем Scalar

namespace RestaurantService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services.AddDataAccessLayer(builder.Configuration);
        builder.Services.AddBusinessLogicLayer();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi(); 
            app.MapScalarApiReference(options => 
            {
                options.WithTitle("Restaurant Service API")
                    .WithTheme(ScalarTheme.Mars);
            });
        }

        app.MapControllers();

        app.Run();
    }
}
