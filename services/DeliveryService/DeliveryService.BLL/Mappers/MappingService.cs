using Microsoft.Extensions.DependencyInjection;
using DeliveryService.BLL.Mappers.Interfaces;
using DeliveryService.BLL.Common;
using DeliveryService.BLL.Exceptions;

namespace DeliveryService.BLL.Mappers;

public class MappingService(IServiceProvider serviceProvider) : IMappingService
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        if (source is null)
            throw new MappingException(typeof(TSource), typeof(TDestination));

        try
        {
            var mapper = serviceProvider.GetRequiredService<IMapper<TSource, TDestination>>();
            return mapper.Map(source);
        }
        catch (InvalidOperationException ex)
        {
            throw new MappingException(typeof(TSource), typeof(TDestination), ex);
        }
    }

    public PagedList<TDestination> MapPagedList<TSource, TDestination>(PagedList<TSource> source)
    {
        if (source is null)
            throw new MappingException(typeof(TSource), typeof(TDestination), isCollection: true);

        try
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
        catch (InvalidOperationException ex)
        {
            throw new MappingException(typeof(TSource), typeof(TDestination), ex);
        }
    }
}
