using System.Diagnostics.CodeAnalysis;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Repository.Implementation;

namespace TaskManagement.API.Extensions
{
    [ExcludeFromCodeCoverage]
    static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            return services;
        }
    }
}