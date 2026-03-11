namespace RestaurantService.BLL.Mappers.Interfaces;

public interface IMappingService
{
    TDestination Map<TSource, TDestination>(TSource source);
}
