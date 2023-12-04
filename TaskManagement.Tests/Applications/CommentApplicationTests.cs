using AutoMapper;
using Bogus;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Implementation;
using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Domain.ViewModel;
using TaskManagement.Repository.Implementation;
using Xunit;

namespace TaskManagement.Tests.Applications
{
    public class CommentApplicationTests
    {
        private IMapper _mapper;
        public CommentApplicationTests()
        {
            _mapper = new MapperConfiguration(config => config.AddProfile(new Domain.Configuration.MapProfile())).CreateMapper();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAsync_ShouldReturnCorrectResult()
        {
            // Arrange
            var commentRepositoryMock = new Mock<ICommentRepository>();
            var commentApplication = new CommentApplication(_mapper, commentRepositoryMock.Object);
            var fakeCommentsData = new Faker<CommentDbModel>().Generate(3);
            commentRepositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<CommentDbModel, bool>>>())).ReturnsAsync(fakeCommentsData);

            // Act
            var result = await commentApplication.GetAsync(1);

            // Assert
            Assert.True(result.IsValid);
            Assert.NotNull(result.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateAsync_ShouldReturnOkResult()
        {
            // Arrange
            var commentRepositoryMock = new Mock<ICommentRepository>();
            var commentApplication = new CommentApplication(_mapper, commentRepositoryMock.Object);
            commentRepositoryMock.Setup(repo => repo.Insert(It.IsAny<CommentDbModel>()));
            var body = new Faker<CommentCreateViewModel>()
                .RuleFor(t => t.Description, f => f.Lorem.Word())
                .Generate(1)
                .First();

            // Act
            var result = await commentApplication.CreateAsync(body, 1, 1);

            // Assert
            Assert.True(result.IsValid);
        }

    }
}
