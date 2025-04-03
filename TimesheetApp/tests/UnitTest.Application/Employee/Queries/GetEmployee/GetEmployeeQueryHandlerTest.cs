using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Employee.Queries.GetEmployee;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Queries.GetEmployee;

public class GetEmployeeQueryHandlerTest
{
    
    [Fact]
    public async Task Handle_ShouldReturnEmployee_WhenExists()
    {
        var testContext = new TestContext();
        var id = Guid.NewGuid();
        var query = new GetEmployeeQuery(id);
        var employee = testContext.GetEmployee(query.Id);
            
        testContext
            .WithEmployeeRepositoryGetSetup(query.Id, employee);
        
        var handler = testContext.CreateHandler;
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(employee.Id, result.Id);
        Assert.Equal(employee.Name, result.Name);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
    {
        var testContext = new TestContext();
        var id = Guid.NewGuid();
        var query = new GetEmployeeQuery(id);
        
        testContext
            .WithEmployeeRepositoryGetSetup(query.Id, null);
        
        var handler = testContext.CreateHandler;

        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }
    
     internal class TestContext
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;

        public TestContext()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
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
        
        public TestContext WithEmployeeRepositoryGetSetup(Guid id, TimesheetApp.Domain.Models.Employee employee)
        {
            _employeeRepository.Setup(repo => repo.Get(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(employee);
            return this;
        }
        
  
        public GetEmployeeQueryHandler CreateHandler => new(_employeeRepository.Object);
    }
}