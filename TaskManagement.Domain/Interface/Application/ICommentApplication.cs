using TaskManagement.Domain.ViewModel;
using TaskManagement.Domain.ViewModel.Result;

namespace TaskManagement.Domain.Interface.Application
{
    public interface ICommentApplication
    {
        Task<Result<IList<CommentGetViewModel>>> GetAsync(int taskId);
        Task<Result> CreateAsync(CommentCreateViewModel commentViewModel, int commentId, int userId);
        Task<Result> UpdateAsync(CommentUpdateViewModel commentViewModel, int commentId, int userId);
        Task<Result> RemoveAsync(int taskId, int commentId);
    }
}