using System.ComponentModel.DataAnnotations;

namespace DeliveryService.DAL.Integrations.Redis;

public class RedisOptions
{
    public const string SectionName = "Redis";

    [Required(ErrorMessage = "ConnectionString must be provided.")]
    public required string ConnectionString { get; set; }
}
