using System.Text.Json;
using DeliveryService.DAL.Outbox;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.DAL;

public static class DependencyInjection
{
    
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddKeyedSingleton<JsonSerializerOptions>(
            OutboxOptions.JsonOptionsKey, 
            new JsonSerializerOptions(JsonSerializerDefaults.Web));

        return services;
    }
}
