using TaskManagement.Domain.ViewModel;
using TaskManagement.Domain.ViewModel.Result;

namespace TaskManagement.Domain.Interface.Application
{
    public interface IProjectApplication
    {
        Task<Result<IList<ProjectGetViewModel>>> GetAsync(int userId);
        Task<Result> CreateAsync(ProjectCreateViewModel projectViewModel, int userId);
        Task<Result> RemoveAsync(int projectId);
    }
}