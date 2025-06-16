using AutoMapper;
using AutoMapper.QueryableExtensions;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;

namespace FleetTrackerSystem.Infrastructure.Repositories.Repos
{
    public static class MapperService
    {
     
        public static IMapper Mapper { get; set; }

        public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source)
        {
            return source.ProjectTo<TDestination>(Mapper.ConfigurationProvider);
        }

        public static IEnumerable<TDestination> ProjectEnumrableTo<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            return source.AsQueryable().ProjectTo<TDestination>(Mapper.ConfigurationProvider);
        }

        public static TDestination Map<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}
