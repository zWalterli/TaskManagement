using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace TaskManagement.API.Extensions
{
    [ExcludeFromCodeCoverage]
    static class MapperExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(config => config.AddProfile(new Domain.Configuration.MapProfile()));
            IMapper mapper = configuration.CreateMapper();
            return services.AddSingleton(mapper);
        }
    }
}