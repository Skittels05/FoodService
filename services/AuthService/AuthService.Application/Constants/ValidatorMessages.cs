namespace AuthService.Application.Constants;

public static class ValidatorMessages
{
    public const string MaxLength = "{PropertyName} cannot exceed {MaxLength} characters.";
    public const string Required = "{PropertyName} is required.";
    public const string InvalidEnum = "Invalid {PropertyName}.";
    public const string MustBeSelected = "{PropertyName} must be selected.";
    public const string InvalidEmail = "A valid email address is required.";
}
