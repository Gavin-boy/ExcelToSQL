using AutoMapper;

namespace ExcelToSQL.Models
{
    public static class AutoMapperExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return default;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());

            var mapper = config.CreateMapper();

            return mapper.Map<TDestination>(source);
        }
    }
}