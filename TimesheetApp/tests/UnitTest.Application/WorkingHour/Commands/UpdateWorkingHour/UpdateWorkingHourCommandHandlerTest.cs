using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.WorkingHour.Commands.UpdateWorkingHour;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Core.Repositories.WorkingHourRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.WorkingHour.Commands.UpdateWorkingHour;

public class UpdateWorkingHourCommandHandlerTest
{
    [Fact]
        public async Task Handle_ShouldUpdateWorkingHour_WhenValidCommand()
        {
            var context = new TestContext();
            var command = TestContext.GetCommand();
            var workingHour = TestContext.GetWorkingHour(command.Id);
            context
                .WithWorkingHourRepositoryGetSetUp(workingHour)
                .WithCategoryExists(command.CategoryId)
                .WithEmployeeExists(command.EmployeeId)
                .WithClientExists(command.ClientId)
                .WithProjectExists(command.ProjectId, command.ClientId, Guid.NewGuid(), [command.EmployeeId])
                .WithWorkingHourRepositoryUpdateAsyncSetup();
            
            var handler = context.CreateHandler;
            var response = await handler.Handle(command, CancellationToken.None);
            
            Assert.NotNull(response);
            Assert.Equal(command.Time, response.Time);
            context.VerifyUpdateAsync(command.Time, Times.Once);
            context.VerifyUnitOfWorkSaveChanges(Times.Once);
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenWorkingHourNotFound()
        {
            var context = new TestContext();
            var command = TestContext.GetCommand();
                
            context.
                WithWorkingHourRepositoryGetSetUp(null!);
                
            var handler = context.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
        {
            var context = new TestContext();
            var command = TestContext.GetCommand();
            var workingHour = TestContext.GetWorkingHour(command.Id);
            
            context
                .WithWorkingHourRepositoryGetSetUp(workingHour)
                .WithEmployeeNotFound(command.EmployeeId);
            
            var handler = context.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenClientNotFound()
        {
            var context = new TestContext();
            var command = TestContext.GetCommand();
            var workingHour = TestContext.GetWorkingHour(command.Id);
            context
                .WithWorkingHourRepositoryGetSetUp(workingHour)
                .WithEmployeeExists(command.EmployeeId)
                   .WithClientNotFound(command.ClientId);
            
            var handler = context.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenProjectNotFound()
        {
            var context = new TestContext();
            var command = TestContext.GetCommand();
            var workingHour = TestContext.GetWorkingHour(command.Id);
            context
                .WithWorkingHourRepositoryGetSetUp(workingHour)
                .WithEmployeeExists(command.EmployeeId)
                   .WithClientExists(command.ClientId)
                   .WithProjectNotFound(command.ProjectId);
            
            var handler = context.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
        
        [Fact]
        public async Task HandleShouldThrowException_WhenCategoryNotFound()
        {
            var context = new TestContext();
            var command = TestContext.GetCommand();
            var workingHour = TestContext.GetWorkingHour(command.Id);
            context
                .WithWorkingHourRepositoryGetSetUp(workingHour)
                .WithEmployeeExists(command.EmployeeId)
                .WithClientExists(command.ClientId)
                .WithProjectExists(command.ProjectId, command.ClientId, Guid.NewGuid(), [command.EmployeeId])
                .WithCategoryNotFound(command.CategoryId);
            
            var handler = context.CreateHandler;
            await Assert.ThrowsAsync<NoDataFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
        
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenClientDoesNotMatchProject()
        {
            var context = new TestContext();
            var command = TestContext.GetCommand();
            var workingHour = TestContext.GetWorkingHour(command.Id);
            
            context
                .WithWorkingHourRepositoryGetSetUp(workingHour)
                   .WithCategoryExists(command.CategoryId)
                   .WithEmployeeExists(command.EmployeeId)
                   .WithClientExists(command.ClientId)
                   .WithProjectExists(command.ProjectId, Guid.NewGuid(), Guid.NewGuid(), [command.EmployeeId]);
            
            var handler = context.CreateHandler;
            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotAssociatedWithProject()
        {
            var context = new TestContext();
            var command = TestContext.GetCommand();
            var workingHour = TestContext.GetWorkingHour(command.Id);
            
            context
                .WithWorkingHourRepositoryGetSetUp(workingHour)
                .WithCategoryExists(command.CategoryId)
                .WithEmployeeExists(command.EmployeeId)
                   .WithClientExists(command.ClientId)
                   .WithProjectExists(command.ProjectId, command.ClientId, Guid.NewGuid(), []);
            
            var handler = context.CreateHandler;
            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handle_ShouldThrowException_WhenDateIsInFuture()
        {
            var context = new TestContext();
            var command = TestContext.GetCommand();
            command.Date = DateTime.UtcNow.AddDays(1);
            var workingHour = TestContext.GetWorkingHour(command.Id);
            
            context
                .WithWorkingHourRepositoryGetSetUp(workingHour)
                .WithCategoryExists(command.CategoryId)
                .WithEmployeeExists(command.EmployeeId)
                   .WithClientExists(command.ClientId)
                   .WithProjectExists(command.ProjectId, command.ClientId, Guid.NewGuid(), [command.EmployeeId]);
            
            var handler = context.CreateHandler;
            await Assert.ThrowsAsync<DateOutOfRangeException>(() => handler.Handle(command, CancellationToken.None));
        }
    
    internal class TestContext
    {
        private readonly Mock<IWorkingHourRepository> _workingHourRepository;
            private readonly Mock<IProjectRepository> _projectRepository;
            private readonly Mock<IEmployeeRepository> _employeeRepository;
            private readonly Mock<IClientRepository> _clientRepository;
            private readonly Mock<ICategoryRepository> _categoryRepository;
            private readonly Mock<IUnitOfWork> _unitOfWork;

            public TestContext()
            {
                _workingHourRepository = new Mock<IWorkingHourRepository>();
                _projectRepository = new Mock<IProjectRepository>();
                _employeeRepository = new Mock<IEmployeeRepository>();
                _clientRepository = new Mock<IClientRepository>();
                _categoryRepository = new Mock<ICategoryRepository>();
                _unitOfWork = new Mock<IUnitOfWork>();
            }

            public static UpdateWorkingHourCommand GetCommand()
            {
                return new UpdateWorkingHourCommand
                {
                    Id = Guid.NewGuid(),
                    Description = "desc",
                    ClientId = Guid.NewGuid(),
                    ProjectId = Guid.NewGuid(),
                    CategoryId = Guid.NewGuid(),
                    EmployeeId = Guid.NewGuid(),
                    Time = 8,
                    Overtime = 0,
                    Date = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)),
                };
            }
            
            public static TimesheetApp.Domain.Models.WorkingHour GetWorkingHour(Guid id)
            {
                return new TimesheetApp.Domain.Models.WorkingHour
                {
                    Id = id,
                    Description = "desc",
                    ClientId = Guid.NewGuid(),
                    EmployeeId = Guid.NewGuid(),
                    Time = 8,
                    ProjectId = Guid.NewGuid(),
                    CategoryId = Guid.NewGuid(),
                    Overtime = 0,
                    Date = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)),
                };
            }
            
            public TestContext WithWorkingHourRepositoryGetSetUp(TimesheetApp.Domain.Models.WorkingHour workingHour)
            {
                _workingHourRepository.Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(workingHour);
                return this;
            }

            public TestContext WithProjectExists(Guid projectId, Guid clientId, Guid leadId, List<Guid> employeeIds)
            {
                _projectRepository.Setup(repo => repo.Get(projectId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new TimesheetApp.Domain.Models.Project
                    {
                        Id = projectId,
                        LeadId = leadId,
                        ClientId = clientId,
                        EmployeeIds = employeeIds,
                        ProjectStatus = ProjectStatus.Active,
                        Description = "Description",
                    });
                return this;
            }

            public TestContext WithProjectNotFound(Guid leadId)
            {
                _projectRepository.Setup(repo => repo.Get(leadId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TimesheetApp.Domain.Models.Project)null!);
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
            
            public TestContext WithCategoryExists(Guid categoryId)
            {
                _categoryRepository.Setup(repo => repo.Get(categoryId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new TimesheetApp.Domain.Models.Category { Id = categoryId, Name = "Test Category" });
                return this;
            }

            public TestContext WithCategoryNotFound(Guid categoryId)
            {
                _categoryRepository.Setup(repo => repo.Get(categoryId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TimesheetApp.Domain.Models.Category)null!);
                return this;
            }

            public TestContext WithWorkingHourRepositoryUpdateAsyncSetup()
            {
                _workingHourRepository.Setup(repo => repo.UpdateAsync(
                        It.IsAny<TimesheetApp.Domain.Models.WorkingHour>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TimesheetApp.Domain.Models.WorkingHour input, CancellationToken _) => input);
                return this;
            }
            
            public void VerifyUpdateAsync(int time, Func<Times> times)
            {
                _workingHourRepository.Verify(repo => repo.UpdateAsync(
                    It.Is<TimesheetApp.Domain.Models.WorkingHour>(c => c.Time == time),
                    It.IsAny<CancellationToken>()), times);
            }
            
            public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
            {
                _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
            }
            
            public UpdateWorkingHourCommandHandler CreateHandler => new(
                _workingHourRepository.Object,
                _projectRepository.Object, 
                _employeeRepository.Object, 
                _clientRepository.Object, 
                _categoryRepository.Object,
                _unitOfWork.Object);
        }
}