using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Repository.Context;

namespace TaskManagement.Repository.Implementation
{
    public class ProjectRepository : BaseRepository<ProjectDbModel>, IProjectRepository
    {
        public ProjectRepository(APIContext context) : base(context)
        {

        }
    }
}