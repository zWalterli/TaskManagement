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

namespace TaskManagement.Tests.Applications
{
    public class ProjectApplicationTests
    {
        private readonly ProjectApplication _projectApplication;
        private readonly Mock<IProjectRepository> _projectRepository;
        public ProjectApplicationTests()
        {
            var mapper = new MapperConfiguration(config => config.AddProfile(new Domain.Configuration.MapProfile())).CreateMapper();
            _projectRepository = new Mock<IProjectRepository>();

            _projectApplication = new ProjectApplication(mapper, _projectRepository.Object);
        }

        #region Get
        [Fact]
        public async Task GetAsync_ShouldReturnSuccess()
        {
            // Arrange
            var userId = 1;
            var fakeProjectDbModel = new Faker<ProjectDbModel>().Generate(3);
            _projectRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<ProjectDbModel, bool>>>())).ReturnsAsync(fakeProjectDbModel);

            // Act
            var result = await _projectApplication.GetAsync(userId);

            // Assert
            Assert.False(result.Notifications.Any());
            Assert.NotEmpty(result.Object!);
        }

        [Fact]
        public async Task GetAsync_ShouldRetornError_UserIdIsInvalid()
        {
            // Arrange
            int userId = -1;

            // Act
            var result = await _projectApplication.GetAsync(userId);

            // Assert
            Assert.True(result.Notifications.Any());
            Assert.Equal("User ID is invalid!", result.Notifications.First().Message);
        }
        #endregion

        #region Create
        [Fact]
        public async Task CreateAsync_ShouldReturnSuccess()
        {
            // Arrange
            var fakeProjectViewModel = new Faker<ProjectCreateViewModel>()
                .RuleFor(p => p.Title, p => p.Lorem.Word())
                .RuleFor(p => p.Description, p => p.Lorem.Word())
                .RuleFor(p => p.UserId, 1)
                .Generate(1);

            // Act
            var result = await _projectApplication.CreateAsync(fakeProjectViewModel.First(), 1);

            // Assert
            Assert.False(result.Notifications.Any());
        }

        [Fact]
        public async Task CreateAsync_DeveRetornarErroQuandoEntidadeInvalida()
        {
            // Arrange
            var projectViewModel = new Faker<ProjectCreateViewModel>().Generate(1).First();
            projectViewModel.Title = string.Empty;

            // Act
            var result = await _projectApplication.CreateAsync(projectViewModel, 1);

            // Assert
            Assert.True(result.Notifications.Any());
        }
        #endregion

        #region Remove
        [Fact]
        public async Task RemoveAsync_ShouldReturnSuccess()
        {
            // Arrange
            int projectId = 1;
            var taskDbModel = new Faker<TaskDbModel>().Generate(3);

            var projectDbModel = new Faker<ProjectDbModel>()
                .RuleFor(p => p.Title, p => p.Lorem.Word())
                .RuleFor(p => p.Description, p => p.Lorem.Word())
                .RuleFor(p => p.UserId, 1)
                .RuleFor(p => p.Id, 1)
                .RuleFor(p => p.Tasks, taskDbModel)
                .Generate(1);

            _projectRepository.Setup(repo => repo.GetWithInclude(
                It.IsAny<Func<ProjectDbModel, bool>>(),
                It.IsAny<Expression<Func<ProjectDbModel, object>>[]>()))
            .ReturnsAsync(projectDbModel);

            // Act
            var result = await _projectApplication.RemoveAsync(projectId);

            // Assert
            Assert.False(result.Notifications.Any());
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnError_WhenProjectIdIsInvalid()
        {
            // Arrange
            int projectId = -1;

            // Act
            var result = await _projectApplication.RemoveAsync(projectId);

            // Assert
            Assert.True(result.Notifications.Any());
            Assert.Equal("Project ID is invalid!", result.Notifications.First().Message);
        }
        #endregion
    }
}