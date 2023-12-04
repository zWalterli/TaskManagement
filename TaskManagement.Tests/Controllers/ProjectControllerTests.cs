using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.API.Controllers;
using TaskManagement.API.Provider;
using TaskManagement.Domain.Interface.Application;
using TaskManagement.Domain.ViewModel;
using TaskManagement.Domain.ViewModel.Result;
using Xunit;

namespace TaskManagement.Tests.Controllers
{
    public class ProjectControllerTests
    {
        private readonly Mock<IProjectApplication> _projectApplication;
        private readonly ProjectController _projectController;
        public ProjectControllerTests()
        {
            var userIdProviderMock = new Mock<IUserIdProvider>();
            userIdProviderMock.Setup(m => m.GetUserId(It.IsAny<HttpContext>())).Returns(1);
            _projectApplication = new Mock<IProjectApplication>();
            _projectController = new ProjectController(_projectApplication.Object, userIdProviderMock.Object);
        }

        #region Get
        [Fact]
        public async Task Get_ReturnsBadRequest_WhenResponseIsInvalid()
        {
            // Arrange
            var invalidResponse = Result<IList<ProjectGetViewModel>>.Error("unitary test error");

            _projectApplication.Setup(x => x.GetAsync(1)).ReturnsAsync(invalidResponse);

            // Act
            var result = await _projectController.Get();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
        }

        [Fact]
        public async Task Get_ReturnsSuccess_WhenResponseIsValid()
        {
            // Arrange
            int userId = 1;
            var validResponse = Result<IList<ProjectGetViewModel>>.Ok(new Faker<ProjectGetViewModel>().Generate(3));

            _projectApplication.Setup(x => x.GetAsync(userId)).ReturnsAsync(validResponse);

            // Act
            var result = await _projectController.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
        }
        #endregion

        #region Create

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenResponseIsInvalid()
        {
            // Arrange
            var invalidResponse = Result.Error("unitary test error");
            var body = new Faker<ProjectCreateViewModel>()
                .RuleFor(p => p.Title, f => f.Lorem.Word())
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.UserId, f => f.Random.Number())
                .Generate(1)
                .First();

            _projectApplication.Setup(x => x.CreateAsync(It.IsAny<ProjectCreateViewModel>(), It.IsAny<int>())).ReturnsAsync(invalidResponse);

            // Act
            var result = await _projectController.Post(body);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
        }

        [Fact]
        public async Task Create_ReturnsOk_WhenResponseIsValid()
        {
            // Arrange
            var validResponse = Result.Ok();
            var body = new Faker<ProjectCreateViewModel>()
                .RuleFor(p => p.Title, f => f.Lorem.Word())
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.UserId, f => f.Random.Number())
                .Generate(1)
                .First();

            _projectApplication.Setup(x => x.CreateAsync(It.IsAny<ProjectCreateViewModel>(), It.IsAny<int>())).ReturnsAsync(validResponse);

            // Act
            var result = await _projectController.Post(body);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Remove_ReturnsBadRequest_WhenResponseIsInvalid()
        {
            // Arrange
            int projectId = 1;
            var invalidResponse = Result.Error("Unitary test error");

            _projectApplication.Setup(x => x.RemoveAsync(projectId)).ReturnsAsync(invalidResponse);

            // Act
            var result = await _projectController.Remove(projectId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsSuccess_WhenResponseIsValid()
        {
            // Arrange
            int projectId = 1;
            var validResponse = Result.Ok();
            _projectApplication.Setup(x => x.RemoveAsync(projectId)).ReturnsAsync(validResponse);

            // Act
            var result = await _projectController.Remove(projectId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion
    }
}
