using RestaurantService.API;
using RestaurantService.API.Extensions;
using RestaurantService.BLL;
using RestaurantService.DAL;
using Scalar.AspNetCore;
using Serilog;

namespace RestaurantService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddApiLogging();

        builder.Services.AddDataAccessLayer(builder.Configuration);
        builder.Services.AddBusinessLogicLayer();
        builder.Services.AddApiLayer();

        var app = builder.Build();

        app.UseApiLogging();
        app.UseExceptionHandler();
        

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("Restaurant Service API")
                    .WithTheme(ScalarTheme.Moon)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            });

            
        }
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
