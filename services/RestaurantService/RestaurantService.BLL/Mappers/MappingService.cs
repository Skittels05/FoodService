using Microsoft.Extensions.DependencyInjection;
using RestaurantService.BLL.Exceptions;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Common;

namespace RestaurantService.BLL.Mappers;

public class MappingService(IServiceProvider serviceProvider) : IMappingService
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        if (source is null)
            throw new MappingException(typeof(TSource), typeof(TDestination));

        var mapper = serviceProvider.GetRequiredService<IMapper<TSource, TDestination>>();

        return mapper.Map(source);
    }
    
    public PagedList<TDestination> MapPagedList<TSource, TDestination>(PagedList<TSource> source)
    {
        if (source is null)
            throw new MappingException(typeof(PagedList<TSource>), typeof(PagedList<TDestination>));

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
