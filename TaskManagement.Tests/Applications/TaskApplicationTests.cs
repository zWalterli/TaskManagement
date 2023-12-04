using Task = System.Threading.Tasks.Task;
using System.Linq.Expressions;
using AutoMapper;
using Xunit;
using Bogus;
using Moq;
using TaskManagement.Application.Implementation;
using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Domain.ViewModel;
using TaskManagement.Domain.Enum;
using Microsoft.AspNetCore.Routing;

namespace TaskManagement.Tests.Applications
{
    public class TaskApplicationTests
    {
        private readonly TaskApplication _taskApplication;
        private readonly Mock<ITaskRepository> _taskRepository;
        private readonly Mock<IProjectRepository> _projectRepository;
        public TaskApplicationTests()
        {
            var mapper = new MapperConfiguration(config => config.AddProfile(new Domain.Configuration.MapProfile())).CreateMapper();
            _taskRepository = new Mock<ITaskRepository>();
            _projectRepository = new Mock<IProjectRepository>();

            _taskApplication = new TaskApplication(mapper, _projectRepository.Object, _taskRepository.Object);
        }

        #region Get
        [Fact]
        public async Task GetAsync_ShouldReturnSuccess()
        {
            // Arrange
            var projectId = 1;
            var fakeTaskDbModel = new Faker<TaskDbModel>().Generate(3);
            _taskRepository.Setup(repo => repo.GetWithInclude(
                It.IsAny<Func<TaskDbModel, bool>>(),
                It.IsAny<Expression<Func<TaskDbModel, object>>[]>()))
            .ReturnsAsync(fakeTaskDbModel);

            // Act
            var result = await _taskApplication.GetAsync(projectId);

            // Assert
            Assert.False(result.Notifications.Any());
            Assert.NotEmpty(result.Object!);
        }
        #endregion

        #region Create
        [Fact]
        public async Task CreateAsync_ShouldReturnSuccess()
        {
            // Arrange
            var taskCreateViewModel = new Faker<TaskCreateViewModel>()
                .RuleFor(t => t.Title, f => f.Lorem.Word())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence())
                .RuleFor(t => t.Priority, f => f.PickRandom<PriorityEnum>())
                .RuleFor(t => t.ProjectId, f => f.Random.Int(1, 999))
                .Generate(1)
                .First();

            var projectDbModel = new Faker<ProjectDbModel>().Generate(1);

            _projectRepository.Setup(repo => repo.GetWithInclude(
                It.IsAny<Func<ProjectDbModel, bool>>(),
                It.IsAny<Expression<Func<ProjectDbModel, object>>[]>()))
            .ReturnsAsync(projectDbModel);

            // Act
            var result = await _taskApplication.CreateAsync(taskCreateViewModel, 1);

            // Assert
            Assert.False(result.Notifications.Any());
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnError_WhenEntityIsNotValid()
        {
            // Arrange
            var taskCreateViewModel = new Faker<TaskCreateViewModel>()
                .Generate(1)
                .First();

            taskCreateViewModel.Title = string.Empty;

            // Act
            var result = await _taskApplication.CreateAsync(taskCreateViewModel, 1);

            // Assert
            Assert.True(result.Notifications.Any());
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnError_WhenProjectNotFound()
        {
            // Arrange
            var taskCreateViewModel = new Faker<TaskCreateViewModel>()
                .RuleFor(t => t.Title, f => f.Lorem.Word())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence())
                .RuleFor(t => t.Priority, f => f.PickRandom<PriorityEnum>())
                .RuleFor(t => t.ProjectId, f => f.Random.Int(1, 999))
                .Generate(1)
                .First();

            _projectRepository.Setup(repo => repo.GetWithInclude(
                It.IsAny<Func<ProjectDbModel, bool>>(),
                It.IsAny<Expression<Func<ProjectDbModel, object>>[]>()))
            .ReturnsAsync(new List<ProjectDbModel>()); // Simula nenhum projeto encontrado.

            // Act
            var result = await _taskApplication.CreateAsync(taskCreateViewModel, 1);

            // Assert
            Assert.True(result.Notifications.Any());
            Assert.Contains($"Project with ID {taskCreateViewModel.ProjectId} not found!", result.Notifications.First().Message);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnError_WhenProjectHas20Tasks()
        {
            // Arrange
            var taskCreateViewModel = new Faker<TaskCreateViewModel>()
                .RuleFor(t => t.Title, f => f.Lorem.Word())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence())
                .RuleFor(t => t.Priority, f => f.PickRandom<PriorityEnum>())
                .RuleFor(t => t.ProjectId, f => f.Random.Int(1, 999))
                .Generate(1)
                .First();

            var projectDbModel = new Faker<ProjectDbModel>()
                .RuleFor(p => p.Tasks, f => Enumerable.Repeat(new TaskDbModel(), 20).ToList()) 
                .Generate(1)
                .First();

            _projectRepository.Setup(repo => repo.GetWithInclude(
                It.IsAny<Func<ProjectDbModel, bool>>(),
                It.IsAny<Expression<Func<ProjectDbModel, object>>[]>()))
            .ReturnsAsync(new List<ProjectDbModel> { projectDbModel });

            // Act
            var result = await _taskApplication.CreateAsync(taskCreateViewModel, 1);

            // Assert
            Assert.True(result.Notifications.Any());
            Assert.Contains("Project already has 20 tasks!", result.Notifications.First().Message);
        }

        #endregion

        #region Update
        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccess()
        {
            // Arrange
            var taskUpdateViewModel = new Faker<TaskUpdateViewModel>()
                .RuleFor(t => t.Id, f => f.Random.Int(1, 10))
                .RuleFor(t => t.Title, f => f.Lorem.Sentence())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence())
                .RuleFor(t => t.Status, f => f.PickRandom<StatusTaskEnum>())
                .Generate(1)
                .First();

            var returnFromGetById = new Faker<TaskDbModel>()
                .RuleFor(t => t.Title, f => f.Lorem.Word())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence())
                .RuleFor(t => t.Status, f => f.PickRandom<StatusTaskEnum>())
                .RuleFor(t => t.Priority, f => f.PickRandom<PriorityEnum>())
                .RuleFor(t => t.UserId, f => f.Random.Number())
                .RuleFor(t => t.ProjectId, f => f.Random.Number())
                .Generate(1)
                .First();

            _taskRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(returnFromGetById);

            // Act
            var result = await _taskApplication.UpdateAsync(taskUpdateViewModel, 1);

            // Assert
            Assert.False(result.Notifications.Any());
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenInvalidData()
        {
            // Arrange
            var taskUpdateViewModel = new Faker<TaskUpdateViewModel>()
                .RuleFor(t => t.Id, 0)
                .RuleFor(t => t.Description, f => f.Lorem.Sentence())
                .RuleFor(t => t.Status, f => f.PickRandom<StatusTaskEnum>())
                .Generate(1)
                .First();

            // Act
            var result = await _taskApplication.UpdateAsync(taskUpdateViewModel, 1);

            // Assert
            Assert.True(result.Notifications.Any());
        }
        #endregion

        #region Remove
        [Fact]
        public async Task RemoveAsync_ShouldReturnSuccess()
        {
            // Arrange
            var taskId = 1;

            var taskDbModel = new Faker<TaskDbModel>()
                .RuleFor(t => t.Id, taskId)
                // Adicione mais regras conforme necessário
                .Generate(1)
                .First();

            _taskRepository.Setup(repo => repo.GetById(taskId))
                              .ReturnsAsync(taskDbModel);

            _taskRepository.Setup(repo => repo.Delete(taskDbModel))
                              .Returns(Task.FromResult(true));

            // Act
            var result = await _taskApplication.RemoveAsync(taskId);

            // Assert
            Assert.False(result.Notifications.Any());
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnError_WhenTaskNotFound()
        {
            // Arrange
            var taskId = 1;

            _taskRepository.Setup(repo => repo.GetById(taskId))
                              .ReturnsAsync((TaskDbModel)null);

            // Act
            var result = await _taskApplication.RemoveAsync(taskId);

            // Assert
            Assert.True(result.Notifications.Any());
            Assert.Equal("Task not found.", result.Notifications.First().Message);
        }
        #endregion
    }
}