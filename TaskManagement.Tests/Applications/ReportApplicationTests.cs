using AutoMapper;
using Bogus;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Implementation;
using TaskManagement.Domain.DbModel;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interface.Application;
using TaskManagement.Domain.Interface.Repository;
using TaskManagement.Repository.Implementation;
using Xunit;

namespace TaskManagement.Tests.Applications
{
    public class ReportApplicationTests
    {
        private readonly Mock<ITaskRepository> _taskRepository;
        private readonly ReportApplication _projectApplication;
        private readonly IMapper _mapper;
        public ReportApplicationTests()
        {
            var mapper = new MapperConfiguration(config => config.AddProfile(new Domain.Configuration.MapProfile())).CreateMapper();
            _taskRepository = new Mock<ITaskRepository>();

            _projectApplication = new ReportApplication(_taskRepository.Object, mapper);
        }
        [Fact]
        public async System.Threading.Tasks.Task GetAmountTasksFinishedPerUserInAMounth_ShouldReturnCorrectResult()
        {
            // Arrange
            var taskRepositoryMock = new Mock<ITaskRepository>();
            var mapperMock = new Mock<IMapper>();

            // Configurar o mock do repositório para retornar dados fictícios
            var fakeTaskData = new Faker<TaskDbModel>().Generate(4);

            taskRepositoryMock.Setup(repo => repo.GetFinishedByUser()).ReturnsAsync(fakeTaskData);

            var reportApplication = new ReportApplication(taskRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await reportApplication.GetAmountTasksFinishedPerUserInAMounth();

            // Assert
            Assert.True(result.IsValid);
            Assert.False(result.Notifications.Any());

            // Adicione mais asserções conforme necessário para verificar se os dados retornados são os esperados
        }
    }
}
