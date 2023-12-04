using System.Diagnostics.CodeAnalysis;
using TaskManagement.Application.Implementation;
using TaskManagement.Domain.Interface.Application;

namespace TaskManagement.API.Extensions
{
    [ExcludeFromCodeCoverage]
    static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskApplication, TaskApplication>();
            services.AddScoped<IReportApplication, ReportApplication>();
            services.AddScoped<IProjectApplication, ProjectApplication>();
            services.AddScoped<ICommentApplication, CommentApplication>();

            return services;
        }
    }
}