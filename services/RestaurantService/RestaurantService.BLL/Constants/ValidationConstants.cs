namespace RestaurantService.BLL.Constants;

public static class ValidationConstants
{
    public const int RestaurantNameMaxLength = 30;
    public const int DocumentFileUrlMaxLength = 50;
    public const int DocumentRejectionReasonMaxLength = 100;
    public const int LocationAddressMaxLength = 100;
    public const int MenuItemNameMaxLength = 30;
    public const int StopListReasonMaxLength = 100;
    public const double LatitudeMin = -90.0;
    public const double LatitudeMax = 90.0;
    public const double LongitudeMin = -180.0;
    public const double LongitudeMax = 180.0;
    public const double RadiusMin = 0.0;
    public const double RadiusMax = 100.0;
}
