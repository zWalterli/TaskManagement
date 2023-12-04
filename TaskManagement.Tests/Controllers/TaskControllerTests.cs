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
using TaskManagement.Domain.Enum;
using TaskManagement.Domain.Interface.Application;
using TaskManagement.Domain.ViewModel;
using TaskManagement.Domain.ViewModel.Result;
using Xunit;

namespace TaskManagement.Tests.Controllers
{
    public class TaskControllerTests
    {
        private readonly Mock<IUserIdProvider> _userIdProviderMock;
        public TaskControllerTests()
        {
            _userIdProviderMock = new Mock<IUserIdProvider>();
            _userIdProviderMock.Setup(m => m.GetUserId(It.IsAny<HttpContext>())).Returns(1);
        }
        [Fact]
        public async Task Get_ReturnsBadRequest_WhenResponseIsInvalid()
        {
            // Arrange
            int projectId = 1;
            var taskApplicationMock = new Mock<ITaskApplication>();
            var commentApplicationMock = new Mock<ICommentApplication>();
            var controller = new TaskController(taskApplicationMock.Object, commentApplicationMock.Object, _userIdProviderMock.Object);
            var invalidResponse = Result<IList<TaskGetViewModel>>.Error("Invalid request");

            taskApplicationMock.Setup(x => x.GetAsync(projectId)).ReturnsAsync(invalidResponse);

            // Act
            var result = await controller.Get(projectId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
        }

        [Fact]
        public async Task Get_ReturnsSuccess_WhenResponseIsValid()
        {
            // Arrange
            int projectId = 1;
            var taskApplicationMock = new Mock<ITaskApplication>();
            var commentApplicationMock = new Mock<ICommentApplication>();
            var controller = new TaskController(taskApplicationMock.Object, commentApplicationMock.Object, _userIdProviderMock.Object);

            var validResponse = Result<IList<TaskGetViewModel>>.Ok(new Faker<TaskGetViewModel>().Generate(3));

            taskApplicationMock.Setup(x => x.GetAsync(projectId)).ReturnsAsync(validResponse);

            // Act
            var result = await controller.Get(projectId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenResponseIsInvalid()
        {
            // Arrange
            var body = new Faker<TaskCreateViewModel>()
            .RuleFor(t => t.Title, f => f.Lorem.Word())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence())
            .Generate();
            var taskApplicationMock = new Mock<ITaskApplication>();
            var commentApplicationMock = new Mock<ICommentApplication>();
            var controller = new TaskController(taskApplicationMock.Object, commentApplicationMock.Object, _userIdProviderMock.Object);

            var invalidResponse = Result.Error("Invalid request");

            taskApplicationMock.Setup(x => x.CreateAsync(body, It.IsAny<int>())).ReturnsAsync(invalidResponse);

            // Act
            var result = await controller.Post(body);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
        }

        [Fact]
        public async Task Post_ReturnsSuccess_WhenResponseIsValid()
        {
            // Arrange
            var body = new Faker<TaskCreateViewModel>()
                .RuleFor(t => t.Title, f => f.Lorem.Word())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence())
                .RuleFor(t => t.Priority, PriorityEnum.Mid)
                .RuleFor(t => t.ProjectId, f => f.Random.Number(1, 10))
                .Generate();

            var taskApplicationMock = new Mock<ITaskApplication>();
            var commentApplicationMock = new Mock<ICommentApplication>();
            var controller = new TaskController(taskApplicationMock.Object, commentApplicationMock.Object, _userIdProviderMock.Object);

            var validResponse = Result.Ok();

            taskApplicationMock.Setup(x => x.CreateAsync(body, It.IsAny<int>())).ReturnsAsync(validResponse);

            // Act
            var result = await controller.Post(body);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenResponseIsInvalid()
        {
            // Arrange
            var body = new Faker<TaskUpdateViewModel>()
            .RuleFor(t => t.Title, f => f.Lorem.Word())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence())
            .Generate();

            var taskApplicationMock = new Mock<ITaskApplication>();
            var commentApplicationMock = new Mock<ICommentApplication>();
            var controller = new TaskController(taskApplicationMock.Object, commentApplicationMock.Object, _userIdProviderMock.Object);

            var invalidResponse = Result.Error("Invalid request");

            taskApplicationMock.Setup(x => x.UpdateAsync(body, It.IsAny<int>())).ReturnsAsync(invalidResponse);

            // Act
            var result = await controller.Put(body);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
        }

        [Fact]
        public async Task Put_ReturnsSuccess_WhenResponseIsValid()
        {
            // Arrange
            var body = new Faker<TaskUpdateViewModel>().Generate();
            var taskApplicationMock = new Mock<ITaskApplication>();
            var commentApplicationMock = new Mock<ICommentApplication>();
            var controller = new TaskController(taskApplicationMock.Object, commentApplicationMock.Object, _userIdProviderMock.Object);

            var validResponse = Result.Ok();

            taskApplicationMock.Setup(x => x.UpdateAsync(body, It.IsAny<int>())).ReturnsAsync(validResponse);

            // Act
            var result = await controller.Put(body);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Remove_ReturnsBadRequest_WhenResponseIsInvalid()
        {
            // Arrange
            int taskId = 1;
            var taskApplicationMock = new Mock<ITaskApplication>();
            var commentApplicationMock = new Mock<ICommentApplication>();
            var controller = new TaskController(taskApplicationMock.Object, commentApplicationMock.Object, _userIdProviderMock.Object);

            var invalidResponse = Result.Error("Invalid request");

            taskApplicationMock.Setup(x => x.RemoveAsync(taskId)).ReturnsAsync(invalidResponse);

            // Act
            var result = await controller.Remove(taskId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
        }

        [Fact]
        public async Task Remove_ReturnsSuccess_WhenResponseIsValid()
        {
            // Arrange
            int taskId = 1;
            var taskApplicationMock = new Mock<ITaskApplication>();
            var commentApplicationMock = new Mock<ICommentApplication>();
            var controller = new TaskController(taskApplicationMock.Object, commentApplicationMock.Object, _userIdProviderMock.Object);

            var validResponse = Result.Ok();

            taskApplicationMock.Setup(x => x.RemoveAsync(taskId)).ReturnsAsync(validResponse);

            // Act
            var result = await controller.Remove(taskId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        #region Comment

        [Fact]
        public async Task GetComment_ValidId_ReturnsOkResult()
        {
            // Arrange
            var mockCommentApplication = new Mock<ICommentApplication>();
            var taskApplicationMock = new Mock<ITaskApplication>();
            var responseMock = new Faker<CommentGetViewModel>().Generate(3);
            mockCommentApplication.Setup(c => c.GetAsync(It.IsAny<int>())).ReturnsAsync(Result<IList<CommentGetViewModel>>.Ok(responseMock));

            var controller = new TaskController(taskApplicationMock.Object, mockCommentApplication.Object, _userIdProviderMock.Object);

            // Act
            var result = await controller.GetComment(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PostComment_ValidData_ReturnsOkResult()
        {
            // Arrange
            var mockCommentApplication = new Mock<ICommentApplication>();
            var taskApplicationMock = new Mock<ITaskApplication>();
            mockCommentApplication.Setup(c => c.CreateAsync(It.IsAny<CommentCreateViewModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Ok());

            var controller = new TaskController(taskApplicationMock.Object, mockCommentApplication.Object, _userIdProviderMock.Object);

            // Act
            var result = await controller.PostComment(new CommentCreateViewModel(), 1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PutComment_ValidData_ReturnsOkResult()
        {
            // Arrange
            var mockCommentApplication = new Mock<ICommentApplication>();
            var taskApplicationMock = new Mock<ITaskApplication>();
            mockCommentApplication.Setup(c => c.UpdateAsync(It.IsAny<CommentUpdateViewModel>(), It.IsAny<int>())).ReturnsAsync(Result.Ok());

            var controller = new TaskController(taskApplicationMock.Object, mockCommentApplication.Object, _userIdProviderMock.Object);

            // Act
            var result = await controller.PutComment(new CommentUpdateViewModel(), 1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RemoveComment_ValidIds_ReturnsOkResult()
        {
            // Arrange
            var mockCommentApplication = new Mock<ICommentApplication>();
            var taskApplicationMock = new Mock<ITaskApplication>();
            mockCommentApplication.Setup(c => c.RemoveAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(Result.Ok());

            var controller = new TaskController(taskApplicationMock.Object, mockCommentApplication.Object, _userIdProviderMock.Object);

            // Act
            var result = await controller.RemoveComment(1, 2);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion
    }
}
