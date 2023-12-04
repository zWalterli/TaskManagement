using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Repository.Context;

namespace TaskManagement.Repository.Implementation
{
    public class TaskRepository : BaseRepository<TaskDbModel>, ITaskRepository
    {
        private readonly APIContext _context;
        public TaskRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<TaskDbModel>> GetFinishedByUser()
        {
            return await _context.Tasks
                .Include(x => x.Comments)
                .Where(x => x.Status == Domain.Enum.StatusTaskEnum.Done && x.CreatedAt >= DateTime.Now.AddMonths(-1))
                .ToListAsync();
        }
    }
}