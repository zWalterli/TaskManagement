using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using TaskManagement.API.Policy;
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
            services.AddAuthorization(options =>
                {
                    options.AddPolicy("UserIdPolicy", policy =>
                    {
                        policy.Requirements.Add(new UserIdRequirement(10));
                    });
                });

            services.AddSingleton<IAuthorizationHandler, UserIdRequirementHandler>();
            return services;
        }
    }
}