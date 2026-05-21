using Microsoft.Extensions.DependencyInjection;
using DeliveryService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Common;

namespace DeliveryService.BLL.Mappers;

public class MappingService(IServiceProvider serviceProvider) : IMappingService
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        
        var mapper = serviceProvider.GetRequiredService<IMapper<TSource, TDestination>>();

        return mapper.Map(source);
    }

    public PagedList<TDestination> MapPagedList<TSource, TDestination>(PagedList<TSource> source)
    {

        var itemMapper = serviceProvider.GetRequiredService<IMapper<TSource, TDestination>>();

        var mappedItems = source.Items.Select(itemMapper.Map).ToList();

        return new PagedList<TDestination>(
            mappedItems,
            source.TotalCount,
            source.PageNumber,
            source.PageSize
        );
    }
}
