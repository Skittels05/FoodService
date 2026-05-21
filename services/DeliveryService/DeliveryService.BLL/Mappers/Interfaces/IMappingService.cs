using RestaurantService.BLL.Common;

namespace DeliveryService.BLL.Mappers.Interfaces;

public interface IMappingService
{
    TDestination Map<TSource, TDestination>(TSource source);
    PagedList<TDestination> MapPagedList<TSource, TDestination>(PagedList<TSource> source);
}
