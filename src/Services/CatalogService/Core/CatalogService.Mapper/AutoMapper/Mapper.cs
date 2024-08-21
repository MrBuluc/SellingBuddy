
using AutoMapper;
using AutoMapper.Internal;

namespace CatalogService.Mapper.AutoMapper
{
    public class Mapper : Application.Interfaces.AutoMapper.IMapper
    {
        public static List<TypePair> typePairs = [];
        private IMapper MapperContainer;

        protected void Config<TDestination, TSource>(int depth = 5, string? ignore = null)
        {
            TypePair typePair = new(typeof(TSource), typeof(TDestination));

            if (typePairs.Any(tp => tp.DestinationType == typePair.DestinationType && tp.SourceType == typePair.SourceType) && ignore is null)
            {
                return;
            }

            typePairs.Add(typePair);
            MapperConfiguration config = new(cfg =>
            {
                foreach (TypePair item in typePairs)
                {
                    if (ignore is not null)
                    {
                        cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ForMember(ignore, member => member.Ignore()).ReverseMap();
                    }
                    else
                    {
                        cfg.CreateMap(item.SourceType, item.DestinationType).MaxDepth(depth).ReverseMap();
                    }
                }
            });

            MapperContainer = config.CreateMapper();
        }

        public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
        {
            Config<TDestination, TSource>(ignore: ignore);
            return MapperContainer.Map<TSource, TDestination>(source);
        }

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
        {
            Config<TDestination, TSource>(ignore: ignore);
            return MapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
        }

        public TDestination Map<TDestination>(object source, string? ignore = null)
        {
            Config<TDestination, object>(ignore: ignore);
            return MapperContainer.Map<TDestination>(source);
        }

        public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
        {
            Config<TDestination, IList<object>>(ignore: ignore);
            return MapperContainer.Map<IList<TDestination>>(source);
        }
    }
}
