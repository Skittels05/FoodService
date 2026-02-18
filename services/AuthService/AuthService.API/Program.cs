using AuthService.API.Infrastructure;
using AuthService.Application;
using AuthService.Infrastructure;
using Scalar.AspNetCore;
using Serilog;
using Destructurama;

namespace AuthService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();
            try
            {
                Log.Information("Starting web application...");
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Destructure.UsingAttributes()
                    .Enrich.FromLogContext());

                builder.Services.AddApplication();
                builder.Services.AddInfrastructure(builder.Configuration);
                builder.Services.AddProblemDetails();
                builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
                builder.Services.AddControllers();
                builder.Services.AddOpenApi();

                var app = builder.Build();

                app.UseSerilogRequestLogging();
                app.UseExceptionHandler();

                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                    app.MapScalarApiReference(options =>
                    {
                        options.WithTitle("AuthService API")
                               .WithTheme(ScalarTheme.Moon)
                               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
                    });
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
