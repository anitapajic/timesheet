using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Employee.Commands.CreateEmployee;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandHandlerTest
{
    [Fact]
    public async Task Handle_ShouldCreateEmployee()
    {
        var testContext = new TestContext();
        var command = TestContext.GetCommand();

        testContext
            .WithEmployeeRepositoryCreateAsyncSetup()
            .WithEmployeeRepositoryExistUsernameSetUp(command.Username, false)
            .WithEmployeeRepositoryExistEmailSetUp(command.Email, false);
        
        var handler = testContext.CreateHandler;
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(command.Name, result.Name);
        
        testContext.VerifyCreateAsync(command.Name, Times.Once);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenUsernameAlreadyExists()
    {
        var testContext = new TestContext();
        var command = TestContext.GetCommand();

        testContext
            .WithEmployeeRepositoryCreateAsyncSetup()
            .WithEmployeeRepositoryExistUsernameSetUp(command.Username, true)
            .WithEmployeeRepositoryExistEmailSetUp(command.Email, false);
        
        var handler = testContext.CreateHandler;
        await Assert.ThrowsAsync<EntityAlreadyExistsException>(
            async () => await handler.Handle(command, CancellationToken.None));
        
        testContext.VerifyCreateAsync(command.Name, Times.Never);
        testContext.VerifyUnitOfWorkSaveChanges(Times.Never);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
    {
        var testContext = new TestContext();
        var command = TestContext.GetCommand();

        testContext
            .WithEmployeeRepositoryCreateAsyncSetup()
            .WithEmployeeRepositoryExistUsernameSetUp(command.Username, false)
            .WithEmployeeRepositoryExistEmailSetUp(command.Email, true);
        
        var handler = testContext.CreateHandler;
        await Assert.ThrowsAsync<EntityAlreadyExistsException>(
            async () => await handler.Handle(command, CancellationToken.None));
        
        testContext.VerifyCreateAsync(command.Name, Times.Never);
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

        public static CreateEmployeeCommand GetCommand()
        {
            var command = new CreateEmployeeCommand
            {
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

        public TestContext WithEmployeeRepositoryCreateAsyncSetup()
        {
            _employeeRepository.Setup(repo => repo.CreateAsync(
                    It.IsAny<TimesheetApp.Domain.Models.Employee>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((TimesheetApp.Domain.Models.Employee input, CancellationToken _) => input);
            return this;
        }
        
        public void VerifyCreateAsync(string name, Func<Times> times)
        {
            _employeeRepository.Verify(repo => repo.CreateAsync(
                It.Is<TimesheetApp.Domain.Models.Employee>(c => c.Name == name),
                It.IsAny<CancellationToken>()), times);
        }
        
        public void VerifyUnitOfWorkSaveChanges(Func<Times> times)
        {
            _unitOfWork.Verify(uof => uof.Save(It.IsAny<CancellationToken>()), times);
        }
        
        public TestContext WithEmployeeRepositoryExistUsernameSetUp(string username, bool exists)
        {
            _employeeRepository.Setup(repo => repo.ExistsByUsernameAsync(username))
                .ReturnsAsync(exists);
            return this;
        }
        public TestContext WithEmployeeRepositoryExistEmailSetUp(string email, bool exists)
        {
            _employeeRepository.Setup(repo => repo.ExistsByEmailAsync(email))
                .ReturnsAsync(exists);
            return this;
        }
        
        public CreateEmployeeCommandHandler CreateHandler => new(_employeeRepository.Object, _unitOfWork.Object);
    }
}