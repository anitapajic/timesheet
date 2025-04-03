using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.WorkingHour.Commands.DeleteWorkingHour;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.WorkingHourRepository;
using Xunit;

namespace UnitTest.Application.WorkingHour.Commands.DeleteWorkingHour;

public class DeleteWorkingHourCommandHandlerTest
{
    [Fact]
    public async Task Handle_ShouldDeleteWorkingHour_WhenExists()
    {
        var id = Guid.NewGuid();
        var command = new DeleteWorkingHourCommand(id);
        var workingHour = TestContext.GetWorkingHour(command.Id);
            
        var testContext = new TestContext()
            .WithWorkingHourRepositoryGetSetUp(workingHour)
            .WithWorkingHourRepositoryDeleteAsyncSetup(workingHour.Id);

        var handler = testContext.CreateHandler;
        await handler.Handle(command, CancellationToken.None);
        
        testContext.VerifyDeleteAsync(workingHour.Id, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenWorkingHourNotFoundForDeletion()
    {
        var id = Guid.NewGuid();
        var command = new DeleteWorkingHourCommand(id);
        
        var testContext = new TestContext()
            .WithWorkingHourRepositoryGetSetUp(null!);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(command, CancellationToken.None));
            
        testContext.VerifyDeleteAsync(command.Id, Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        
    }
    
     internal class TestContext
        {
            private readonly Mock<IWorkingHourRepository> _workingHourRepository;
            private readonly Mock<IUnitOfWork> _unitOfWork;

            public TestContext()
            {
                _workingHourRepository = new Mock<IWorkingHourRepository>();
                _unitOfWork = new Mock<IUnitOfWork>();
            }
            

            public static TimesheetApp.Domain.Models.WorkingHour GetWorkingHour(Guid id)
            {
                return new TimesheetApp.Domain.Models.WorkingHour
                {
                    Id = id,
                    ClientId = Guid.NewGuid(),
                    EmployeeId = Guid.NewGuid(),
                    Time = 8,
                    ProjectId = Guid.NewGuid(),
                    CategoryId = Guid.NewGuid(),
                };
            }

            public TestContext WithWorkingHourRepositoryGetSetUp(TimesheetApp.Domain.Models.WorkingHour workingHour)
            {
                _workingHourRepository.Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(workingHour);
                return this;
            }

            public TestContext WithWorkingHourRepositoryDeleteAsyncSetup(Guid id)
            {
                _workingHourRepository.Setup(repo => repo.DeleteAsync(
                        id, It.IsAny<CancellationToken>()));
                return this;
            }
            
            public void VerifyDeleteAsync(Guid id, Func<Times> times)
            {
                _workingHourRepository.Verify(repo => repo.DeleteAsync(
                    id,
                    It.IsAny<CancellationToken>()), times);
            }
            
            public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
            {
                _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
            }
            
            public DeleteWorkingHourCommandHandler CreateHandler => new(
                _workingHourRepository.Object, 
                _unitOfWork.Object);
        }
}