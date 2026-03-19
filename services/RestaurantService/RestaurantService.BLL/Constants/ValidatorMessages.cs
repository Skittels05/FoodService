namespace RestaurantService.BLL.Constants;

public static class ValidatorMessages
{
    public const string Required = "{PropertyName} is required.";
    public const string MaxLength = "{PropertyName} cannot exceed {MaxLength} characters.";
    public const string InvalidEnum = "Invalid {PropertyName}.";
    public const string MustBeSelected = "{PropertyName} must be selected.";
    public const string InvalidFormat = "{PropertyName} has an invalid format.";
    public const string InvalidRange = "{PropertyName} must be between {From} and {To}.";
    public const string GreaterThan = "{PropertyName} must be greater than {ComparisonValue}.";
}
