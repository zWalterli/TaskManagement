using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interface.Application;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Domain.ViewModel;
using TaskManagement.Domain.ViewModel.Result;
using AutoMapper;

namespace TaskManagement.Application.Implementation
{
    public class ProjectApplication : IProjectApplication
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        public ProjectApplication(IMapper mapper, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<Result<IList<ProjectGetViewModel>>> GetAsync(int userId)
        {
            if (userId < 0)
                return Result<IList<ProjectGetViewModel>>.Error("User ID is invalid!");

            var projects = await _projectRepository.Get(x => x.UserId == userId);
            var response = _mapper.Map<IList<ProjectGetViewModel>>(
                _mapper.Map<IList<Project>>(projects)
            );

            return Result<IList<ProjectGetViewModel>>.Ok(response);
        }

        public async Task<Result> CreateAsync(ProjectCreateViewModel projectViewModel, int userId)
        {
            projectViewModel.UserId = userId;
            var entity = _mapper.Map<Project>(projectViewModel);
            if (entity.IsValidToCreate() is false)
                return Result.Error(entity.Notifications);

            var dbModel = _mapper.Map<ProjectDbModel>(entity);
            await _projectRepository.Insert(dbModel);

            return Result.Ok();
        }

        public async Task<Result> RemoveAsync(int projectId)
        {
            if (projectId < 0)
                return Result.Error("Project ID is invalid!");

            var projects = await _projectRepository.GetWithInclude(x => x.Id == projectId, x => x.Tasks);
            if (!projects.Any())
                return Result.Error("Project ID not found!");

            var project = projects.First();
            var isThereTaskWithStatusPending = project.Tasks == null || !project.Tasks.Any() ? false : project.Tasks.Any(x => x.Status == Domain.Enum.StatusTaskEnum.Doing || x.Status == Domain.Enum.StatusTaskEnum.ToDo);
            if (isThereTaskWithStatusPending)
                return Result.Error("There are tasks with todo/doing status, recommend removing or completing these tasks.", "Error on try to delete project!");

            await _projectRepository.Delete(project);

            return Result.Ok();
        }
    }
}