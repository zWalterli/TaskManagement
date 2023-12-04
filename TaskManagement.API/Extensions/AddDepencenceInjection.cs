using System.Diagnostics.CodeAnalysis;
using TaskManagement.API.Provider;

namespace TaskManagement.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AddDepencenceInjection
    {
        public static IServiceCollection AddObject(this IServiceCollection services, IConfiguration configuration)
        {
            AddSingleton(services);
            AddScoped(services, configuration);

            return services;
        }

        private static IServiceCollection AddScoped(IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        private static IServiceCollection AddSingleton(IServiceCollection services)
        {
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            return services;
        }
    }
}