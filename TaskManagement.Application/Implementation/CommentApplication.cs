using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Interface.Application;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Domain.ViewModel.Result;
using TaskManagement.Domain.ViewModel;
using TaskManagement.Domain.Entities;
using AutoMapper;

namespace TaskManagement.Application.Implementation
{
    public class CommentApplication : ICommentApplication
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        public CommentApplication(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<Result<IList<CommentGetViewModel>>> GetAsync(int taskId)
        {
            var commentsDb = await _commentRepository.Get(x => x.TaskId == taskId);
            var response = _mapper.Map<IList<CommentGetViewModel>>(
                _mapper.Map<IList<Comment>>(commentsDb)
            );

            return Result<IList<CommentGetViewModel>>.Ok(response);
        }

        public async Task<Result> CreateAsync(CommentCreateViewModel commentViewModel, int taskId, int userId)
        {
            var entity = _mapper.Map<Comment>(commentViewModel);
            entity.TaskId = taskId;
            entity.UserId = userId;
            if (entity.IsValidToCreate() is false)
                return Result.Error(entity.Notifications);

            var dbModel = _mapper.Map<CommentDbModel>(entity);
            await _commentRepository.Insert(dbModel);

            return Result.Ok();
        }

        public async Task<Result> UpdateAsync(CommentUpdateViewModel commentViewModel, int taskId, int userId)
        {
            var entity = _mapper.Map<Comment>(commentViewModel);
            entity.TaskId = taskId;
            entity.UserId = userId;
            if (entity.IsValidToUpdate() is false)
                return Result.Error(entity.Notifications);

            var dbModel = await _commentRepository.GetById(commentViewModel.Id);
            dbModel.Description = commentViewModel.Description;
            await _commentRepository.Update(dbModel);

            return Result.Ok();
        }

        public async Task<Result> RemoveAsync(int commentId, int taskId)
        {
            var dbModels = await _commentRepository.Get(x => x.Id == commentId && x.TaskId == taskId);
            if (dbModels is null || !dbModels.Any())
                return Result.Error("Comment not found.");

            await _commentRepository.Delete(dbModels.First());
            return Result.Ok();
        }
    }
}