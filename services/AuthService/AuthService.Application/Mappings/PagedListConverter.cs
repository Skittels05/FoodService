using AutoMapper;
using AuthService.Domain.Common;

namespace AuthService.Application.Mappings;

public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
{
    public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
    {
        var mappedItems = context.Mapper.Map<List<TDestination>>(source.Items);
        return new PagedList<TDestination>(
            mappedItems,
            source.TotalCount,
            source.PageNumber,
            source.PageSize);
    }
}
