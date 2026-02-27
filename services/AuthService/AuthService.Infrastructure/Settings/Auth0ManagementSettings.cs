using AuthService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Infrastructure.Settings;

public class Auth0ManagementSettings: IValidatableObject
{
    [Required]
    public string Domain { get; set; } = string.Empty;

    [Required]
    public string ClientId { get; set; } = string.Empty;

    [Required]
    public string ClientSecret { get; set; } = string.Empty;

    [Required]
    public Dictionary<string, string> Roles { get; set; } = new();
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var requiredRoleNames = Enum.GetNames<UserRole>().Where(r => r is not nameof(UserRole.None));

        foreach (var roleName in requiredRoleNames)
        {
            if (Roles.TryGetValue(roleName, out var roleId) is false || string.IsNullOrWhiteSpace(roleId))
            {
                yield return new ValidationResult(
                    $"Критическая ошибка: В appsettings.json отсутствует ID для роли '{roleName}'.",
                    [nameof(Roles)]
                );
            }
        }
    }
}
