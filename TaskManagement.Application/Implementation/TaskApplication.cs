using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Interface.Application;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Domain.ViewModel;
using TaskManagement.Domain.ViewModel.Result;
using AutoMapper;

namespace TaskManagement.Application.Implementation
{
    public class TaskApplication : ITaskApplication
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        public TaskApplication(IMapper mapper, IProjectRepository projectRepository, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<Result<IList<TaskGetViewModel>>> GetAsync(int projectId)
        {
            var tasksFromProject = await _taskRepository.GetWithInclude(x => x.ProjectId == projectId, x => x.Comments);
            var response = _mapper.Map<IList<TaskGetViewModel>>(
                _mapper.Map<IList<Domain.Entities.Task>>(tasksFromProject)
            );

            return Result<IList<TaskGetViewModel>>.Ok(response);
        }

        public async Task<Result> CreateAsync(TaskCreateViewModel taskViewModel, int userId)
        {
            var entity = _mapper.Map<Domain.Entities.Task>(taskViewModel);
            entity.UserId = userId;
            if (entity.IsValidToCreate() is false)
                return Result.Error(entity.Notifications);

            var projects = await _projectRepository.GetWithInclude(x => x.Id == taskViewModel.ProjectId, x => x.Tasks);
            if (!projects.Any())
                return Result.Error($"Project with ID {taskViewModel.ProjectId} not found!");

            var project = projects.First();
            if (project.Tasks != null && project.Tasks.Count >= 20)
                return Result.Error("Project already has 20 tasks!");

            entity.SetEntityToCreateTask();
            var dbModel = _mapper.Map<TaskDbModel>(entity);
            await _taskRepository.Insert(dbModel);

            return Result.Ok();
        }

        public async Task<Result> UpdateAsync(TaskUpdateViewModel taskViewModel, int userId)
        {
            var entity = _mapper.Map<Domain.Entities.Task>(taskViewModel);
            entity.UserId = userId;
            if (entity.IsValidToUpdate() is false)
                return Result.Error(entity.Notifications);

            var taskDb = await _taskRepository.GetById(entity.Id.Value);
            taskDb.Title = entity.Title;
            taskDb.Description = entity.Description;
            taskDb.Status = entity.Status;

            await _taskRepository.Update(taskDb);

            return Result.Ok();
        }

        public async Task<Result> RemoveAsync(int taskId)
        {
            var dbModel = await _taskRepository.GetById(taskId);
            if (dbModel is null)
                return Result.Error("Task not found.");

            await _taskRepository.Delete(dbModel);
            return Result.Ok();
        }
    }
}