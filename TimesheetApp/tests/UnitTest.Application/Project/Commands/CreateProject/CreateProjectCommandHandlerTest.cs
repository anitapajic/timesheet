using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Project.Commands.CreateProject;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Core.Repositories.ProjectRepository;
using Xunit;

namespace UnitTest.Application.Project.Commands.CreateProject;

public class CreateProjectCommandHandlerTest
{
    [Fact]
        public async Task Handle_ShouldCreateProject()
        {
            var testContext = new TestContext();
            var command = TestContext.GetCommand();

            testContext
                .WithEmployeeExists(command.LeadId)
                .WithClientExists(command.ClientId)
                .WithProjectRepositoryCreateAsyncSetup();
            
            var handler = testContext.CreateHandler;
            var result = await handler.Handle(command, CancellationToken.None);
            
            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            
            testContext.VerifyCreateAsync(command.Name, Times.Once);
            testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
        {
            var testContext = new TestContext();
            var command = TestContext.GetCommand();
            
            testContext
                .WithEmployeeNotFound(command.LeadId)
                .WithClientExists(command.ClientId);
            
            var handler = testContext.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(
                async () => await handler.Handle(command, CancellationToken.None));
            
            testContext.VerifyCreateAsync(command.Name, Times.Never);
            testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenClientNotFound()
        {
            var testContext = new TestContext();
            var command = TestContext.GetCommand();
            
            testContext
                .WithEmployeeExists(command.LeadId)
                .WithClientNotFound(command.ClientId);
            
            var handler = testContext.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(
                async () => await handler.Handle(command, CancellationToken.None));
            
            testContext.VerifyCreateAsync(command.Name, Times.Never);
            testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        }
        
        internal class TestContext
        {
            private readonly Mock<IProjectRepository> _projectRepository;
            private readonly Mock<IEmployeeRepository> _employeeRepository;
            private readonly Mock<IClientRepository> _clientRepository;
            private readonly Mock<IUnitOfWork> _unitOfWork;

            public TestContext()
            {
                _projectRepository = new Mock<IProjectRepository>();
                _employeeRepository = new Mock<IEmployeeRepository>();
                _clientRepository = new Mock<IClientRepository>();
                _unitOfWork = new Mock<IUnitOfWork>();
            }

            public static CreateProjectCommand GetCommand()
            {
                return new CreateProjectCommand
                {
                    Name = "Test Project",
                    Description = "Project Description",
                    LeadId = Guid.NewGuid(),
                    ClientId = Guid.NewGuid()
                };
            }

            public TestContext WithEmployeeExists(Guid leadId)
            {
                _employeeRepository.Setup(repo => repo.Get(leadId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new TimesheetApp.Domain.Models.Employee { Id = leadId, Email = "email@example.com", Username = "username", Password = "123456"});
                return this;
            }

            public TestContext WithEmployeeNotFound(Guid leadId)
            {
                _employeeRepository.Setup(repo => repo.Get(leadId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TimesheetApp.Domain.Models.Employee)null!);
                return this;
            }

            public TestContext WithClientExists(Guid clientId)
            {
                _clientRepository.Setup(repo => repo.Get(clientId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new TimesheetApp.Domain.Models.Client { Id = clientId, Name = "Test Client" });
                return this;
            }

            public TestContext WithClientNotFound(Guid clientId)
            {
                _clientRepository.Setup(repo => repo.Get(clientId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TimesheetApp.Domain.Models.Client)null!);
                return this;
            }

            public TestContext WithProjectRepositoryCreateAsyncSetup()
            {
                _projectRepository.Setup(repo => repo.CreateAsync(
                        It.IsAny<TimesheetApp.Domain.Models.Project>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TimesheetApp.Domain.Models.Project input, CancellationToken _) => input);
                return this;
            }
            
            public void VerifyCreateAsync(string name, Func<Times> times)
            {
                _projectRepository.Verify(repo => repo.CreateAsync(
                    It.Is<TimesheetApp.Domain.Models.Project>(c => c.Name == name),
                    It.IsAny<CancellationToken>()), times);
            }
            
            public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
            {
                _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
            }
            
            public CreateProjectCommandHandler CreateHandler => new(
                _projectRepository.Object, 
                _employeeRepository.Object, 
                _clientRepository.Object, 
                _unitOfWork.Object);
        }
}