using Microsoft.Extensions.DependencyInjection;
using RestaurantService.BLL.Mappers.Interfaces;

namespace RestaurantService.BLL.Mappers;

public class MappingService(IServiceProvider serviceProvider) : IMappingService
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var mapper = serviceProvider.GetRequiredService<IMapper<TSource, TDestination>>();

        return mapper.Map(source);
    }
}
