using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Project.Commands.UpdateProject;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Project.Commands.UpdateProject;

public class UpdateProjectCommandHandlerTest
{
    
    [Fact]
        public async Task Handle_ShouldUpdateProject()
        {
            var testContext = new TestContext();
            var command = TestContext.GetCommand();
            var project = TestContext.GetProject(command.Id, command.LeadId, command.ClientId, command.EmployeeIds!);
            
            testContext
                .WithProjectRepositoryGetSetUp(command.Id, project)
                .WithEmployeeExists(command.LeadId)
                .WithClientExists(command.ClientId)
                .WithEmployeesExists(command.EmployeeIds![0], command.EmployeeIds[1])
                .WithProjectRepositoryUpdateAsyncSetup();
            
            var handler = testContext.CreateHandler;
            var result = await handler.Handle(command, CancellationToken.None);
            
            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            
            testContext.VerifyUpdateAsync(command.Name, Times.Once);
            testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
        {
            var testContext = new TestContext();
            var command = TestContext.GetCommand();
            var project = TestContext.GetProject(command.Id, command.LeadId, command.ClientId, command.EmployeeIds!);
            
            testContext
                .WithProjectRepositoryGetSetUp(command.Id, project)
                .WithEmployeeNotFound(command.LeadId)
                .WithClientExists(command.ClientId)
                .WithEmployeesExists(command.EmployeeIds![0], command.EmployeeIds[1]);
            
            var handler = testContext.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(
                async () => await handler.Handle(command, CancellationToken.None));
            
            testContext.VerifyUpdateAsync(command.Name, Times.Never);
            testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenClientNotFound()
        {
            var testContext = new TestContext();
            var command = TestContext.GetCommand();
            var project = TestContext.GetProject(command.Id, command.LeadId, command.ClientId, command.EmployeeIds!);
            
            testContext
                .WithProjectRepositoryGetSetUp(command.Id, project)
                .WithEmployeeExists(command.LeadId)
                .WithClientNotFound(command.ClientId)
                .WithEmployeesExists(command.EmployeeIds![0], command.EmployeeIds[1]);
            
            var handler = testContext.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(
                async () => await handler.Handle(command, CancellationToken.None));
            
            testContext.VerifyUpdateAsync(command.Name, Times.Never);
            testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenProjectNotFound()
        {
            var testContext = new TestContext();
            var command = TestContext.GetCommand();
            
            testContext
                .WithProjectRepositoryGetSetUp(command.Id, null!)
                .WithEmployeeExists(command.LeadId)
                .WithClientExists(command.ClientId)
                .WithEmployeesExists(command.EmployeeIds![0], command.EmployeeIds[1]);
            
            var handler = testContext.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(
                async () => await handler.Handle(command, CancellationToken.None));
            
            testContext.VerifyUpdateAsync(command.Name, Times.Never);
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

            public static UpdateProjectCommand GetCommand()
            {
                return new UpdateProjectCommand
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Project",
                    Description = "Project Description",
                    LeadId = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    ProjectStatus = ProjectStatus.Active,
                    EmployeeIds = [Guid.NewGuid(), Guid.NewGuid()]
                };
            }

            public static TimesheetApp.Domain.Models.Project GetProject(Guid id, Guid leadId, Guid clientId, List<Guid> employeeIds)
            {
                return new TimesheetApp.Domain.Models.Project
                {
                    Id = id,
                    LeadId = leadId,
                    ClientId = clientId,
                    EmployeeIds = employeeIds,
                    ProjectStatus = ProjectStatus.Active,
                    Description = "Description",
                };
            }

            public TestContext WithProjectRepositoryGetSetUp(Guid projectId, TimesheetApp.Domain.Models.Project project)
            {
                _projectRepository.Setup(repo => repo.Get(projectId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(project);
                return this;
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

            public TestContext WithEmployeesExists(Guid id1, Guid id2)
            {
                _employeeRepository.Setup(repo =>
                        repo.GetByIdsAsync(new List<Guid> { id1, id2 }, It.IsAny<CancellationToken>()))
                    .ReturnsAsync([
                        new()
                        {
                            Id = id1, Name = "name1", Username = "username1", Email = "email1@mail.com",
                            Password = "123456", HoursPerWeek = 40, Role = Role.Worker,
                            EmployeeStatus = EmployeeStatus.Active
                        },

                        new()
                        {
                            Id = id2, Name = "name2", Username = "username2", Email = "email2@mail.com",
                            Password = "123456", HoursPerWeek = 40, Role = Role.Worker,
                            EmployeeStatus = EmployeeStatus.Active
                        }
                    ]);
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

            public TestContext WithProjectRepositoryUpdateAsyncSetup()
            {
                _projectRepository.Setup(repo => repo.UpdateAsync(
                        It.IsAny<TimesheetApp.Domain.Models.Project>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TimesheetApp.Domain.Models.Project input, CancellationToken _) => input);
                return this;
            }
            
            public void VerifyUpdateAsync(string name, Func<Times> times)
            {
                _projectRepository.Verify(repo => repo.UpdateAsync(
                    It.Is<TimesheetApp.Domain.Models.Project>(c => c.Name == name),
                    It.IsAny<CancellationToken>()), times);
            }
            
            public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
            {
                _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
            }
            
            public UpdateProjectCommandHandler CreateHandler => new(
                _projectRepository.Object, 
                _employeeRepository.Object, 
                _clientRepository.Object, 
                _unitOfWork.Object);
        }
}