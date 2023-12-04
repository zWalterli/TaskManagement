using TaskManagement.Domain.DbModel;

namespace TaskManagement.Domain.Interface.Repository
{
    public interface ITaskRepository : IBaseRepository<TaskDbModel>
    {
        Task<IList<TaskDbModel>> GetFinishedByUser();
    }
}