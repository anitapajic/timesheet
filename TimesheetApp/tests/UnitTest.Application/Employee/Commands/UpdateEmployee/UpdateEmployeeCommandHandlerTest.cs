using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Employee.Commands.UpdateEmployee;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandlerTest
{
    
    [Fact]
    public async Task Handle_ShouldUpdateEmployee()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();
        var employee = testContext.GetEmployee(command.Id);

        testContext
            .WithEmployeeRepositoryGetSetup(command.Id, employee)
            .WithEmployeeRepositoryUpdateAsyncSetUp();

        var handler = testContext.CreateHandler;
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);

        testContext.VerifyUpdateAsync(command.Id, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
    {
        var testContext = new TestContext();
        var command = testContext.GetCommand();

        testContext
            .WithEmployeeRepositoryGetSetup(command.Id, null);

        var handler = testContext.CreateHandler;
        await Assert.ThrowsAsync<NoDataFoundException>(
            async () => await handler.Handle(command, CancellationToken.None));
            
        testContext.VerifyUpdateAsync(command.Id, Times.Never);
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

        public UpdateEmployeeCommand GetCommand()
        {
            var command = new UpdateEmployeeCommand
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Username = "username",
                Email = "email@mail.com",
                Password = "123456",
                HoursPerWeek = 40,
                Role = Role.Worker,
                EmployeeStatus = EmployeeStatus.Active
            };

            return command;
        }
        
        public TimesheetApp.Domain.Models.Employee GetEmployee(Guid id)
        {
            var command = new TimesheetApp.Domain.Models.Employee
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

            return command;
        }

        public TestContext WithEmployeeRepositoryUpdateAsyncSetUp()
        {
            _employeeRepository.Setup(repo => repo.UpdateAsync(
                    It.IsAny<TimesheetApp.Domain.Models.Employee>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((TimesheetApp.Domain.Models.Employee input, CancellationToken _) => input);
            return this;
        }
        
        public TestContext WithEmployeeRepositoryGetSetup(Guid id, TimesheetApp.Domain.Models.Employee employee)
        {
            _employeeRepository.Setup(repo => repo.Get(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(employee);
            return this;
        }
        
        public void VerifyUpdateAsync(Guid id, Func<Times> times)
        {
            _employeeRepository.Verify(repo => repo.UpdateAsync(
                It.Is<TimesheetApp.Domain.Models.Employee>(c => c.Id == id),
                It.IsAny<CancellationToken>()), times);
        }
        
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }
        
        public UpdateEmployeeCommandHandler CreateHandler => new(_employeeRepository.Object, _unitOfWork.Object);
    }
}