using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Employee.Commands.DeleteEmployee;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandlerTest
{
    [Fact]
    public async Task Handle_ShouldDeleteEmployee_WhenExists()
    {
        var id = Guid.NewGuid();
        var command = new DeleteEmployeeCommand(id);
        var employee = new TimesheetApp.Domain.Models.Employee
        {
            Id = id,
            Name = "name",
            Username = "username",
            Email = "email@mail.com",
            Password = "123456",
            HoursPerWeek = 40,
            Role = Role.Worker,
            EmployeeStatus = EmployeeStatus.Active
        };
            
        var testContext = new TestContext()
            .WithEmployeeRepositoryGetSetup(employee)
            .WithEmployeeRepositoryDeleteAsyncSetup(employee.Id);

        var handler = testContext.CreateHandler;
        await handler.Handle(command, CancellationToken.None);
        
        testContext.VerifyDeleteAsync(employee.Id, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmployeeNotFoundForDeletion()
    {
        var id = Guid.NewGuid();
        var command = new DeleteEmployeeCommand(id);
        
        var testContext = new TestContext()
            .WithEmployeeRepositoryGetSetup(null!);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(command, CancellationToken.None));
            
        testContext.VerifyDeleteAsync(command.Id, Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
        
    }
    
     internal class TestContext
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public TestContext()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }
        
        public TestContext WithEmployeeRepositoryGetSetup(TimesheetApp.Domain.Models.Employee employee)
        {
            _employeeRepository.Setup(repo => repo.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(employee);
            return this;
        }
        
        public TestContext WithEmployeeRepositoryDeleteAsyncSetup(Guid id)
        {
            _employeeRepository.Setup(repo => repo.DeleteAsync(
                id,
                It.IsAny<CancellationToken>()));
            return this;
        }
        
        public void VerifyDeleteAsync(Guid id, Func<Times> times)
        {
            _employeeRepository.Verify(repo => repo.DeleteAsync(
                id,
                It.IsAny<CancellationToken>()), times);
        }
        
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }
        
        public DeleteEmployeeCommandHandler CreateHandler => new(_employeeRepository.Object, _unitOfWork.Object);
    }
}