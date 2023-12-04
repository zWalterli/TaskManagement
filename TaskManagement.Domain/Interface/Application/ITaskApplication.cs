using TaskManagement.Domain.ViewModel;
using TaskManagement.Domain.ViewModel.Result;

namespace TaskManagement.Domain.Interface.Application
{
    public interface ITaskApplication
    {
        Task<Result<IList<TaskGetViewModel>>> GetAsync(int projectId);
        Task<Result> CreateAsync(TaskCreateViewModel taskViewModel, int userId);
        Task<Result> UpdateAsync(TaskUpdateViewModel taskViewModel, int userId);
        Task<Result> RemoveAsync(int taskId);
    }
}