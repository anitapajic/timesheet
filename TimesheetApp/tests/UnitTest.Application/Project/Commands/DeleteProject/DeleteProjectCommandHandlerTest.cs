using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Project.Commands.DeleteProject;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Project.Commands.DeleteProject;

public class DeleteProjectCommandHandlerTest
{
    [Fact]
    public async Task Handle_ShouldDeleteProject_WhenExists()
    {
        var id = Guid.NewGuid();
        var command = new DeleteProjectCommand(id);
        var project = TestContext.GetProject(command.Id);
            
        var testContext = new TestContext()
            .WithProjectRepositoryGetSetUp(project)
            .WithProjectRepositoryDeleteAsyncSetup(project.Id);

        var handler = testContext.CreateHandler;
        await handler.Handle(command, CancellationToken.None);
        
        testContext.VerifyDeleteAsync(project.Id, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProjectNotFoundForDeletion()
    {
        var id = Guid.NewGuid();
        var command = new DeleteProjectCommand(id);
        
        var testContext = new TestContext()
            .WithProjectRepositoryGetSetUp(null!);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(command, CancellationToken.None));
            
        testContext.VerifyDeleteAsync(command.Id, Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        
    }
    
     internal class TestContext
        {
            private readonly Mock<IProjectRepository> _projectRepository;
            private readonly Mock<IUnitOfWork> _unitOfWork;

            public TestContext()
            {
                _projectRepository = new Mock<IProjectRepository>();
                _unitOfWork = new Mock<IUnitOfWork>();
            }
            

            public static TimesheetApp.Domain.Models.Project GetProject(Guid id)
            {
                return new TimesheetApp.Domain.Models.Project
                {
                    Id = id,
                    LeadId = Guid.NewGuid(),
                    ClientId = Guid.NewGuid(),
                    EmployeeIds = [Guid.NewGuid(), Guid.NewGuid()],
                    ProjectStatus = ProjectStatus.Active,
                    Description = "Description",
                };
            }

            public TestContext WithProjectRepositoryGetSetUp(TimesheetApp.Domain.Models.Project project)
            {
                _projectRepository.Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(project);
                return this;
            }

            public TestContext WithProjectRepositoryDeleteAsyncSetup(Guid id)
            {
                _projectRepository.Setup(repo => repo.DeleteAsync(
                        id, It.IsAny<CancellationToken>()));
                return this;
            }
            
            public void VerifyDeleteAsync(Guid id, Func<Times> times)
            {
                _projectRepository.Verify(repo => repo.DeleteAsync(
                    id,
                    It.IsAny<CancellationToken>()), times);
            }
            
            public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
            {
                _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
            }
            
            public DeleteProjectCommandHandler CreateHandler => new(
                _projectRepository.Object, 
                _unitOfWork.Object);
        }
}