using NSubstitute;
using TimesheetApp.Application.Employee.Queries.ListEmployees;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Queries.ListEmployees;

public class ListEmployeesQueryHandlerTest
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ListEmployeesQueryHandler _handler;
    
    public ListEmployeesQueryHandlerTest()
    {
        _employeeRepository = Substitute.For<IEmployeeRepository>();
        _handler = new ListEmployeesQueryHandler(_employeeRepository);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnListOfEmployees()
    {
        var query = new ListEmployeesQuery();
        var employees = new List<TimesheetApp.Domain.Models.Employee>
        {
            new() { Id = Guid.NewGuid(), Name = "name1", Username = "username1", Email = "email1@mail.com", Password = "123456", HoursPerWeek = 40, Role = Role.Worker, EmployeeStatus = EmployeeStatus.Active},
            new() {  Id = Guid.NewGuid(), Name = "name2", Username = "username2", Email = "email2@mail.com", Password = "123456", HoursPerWeek = 40,  Role = Role.Worker, EmployeeStatus = EmployeeStatus.Active}
        };
            
        _employeeRepository.GetAll(Arg.Any<CancellationToken>()).Returns(employees);
            
        var result = await _handler.Handle(query, CancellationToken.None);
            
        Assert.NotNull(result);
        Assert.Equal(employees.Count, result.Count);
        Assert.Equal(employees[0].Id, result[0].Id);
        Assert.Equal(employees[0].Name, result[0].Name);
        Assert.Equal(employees[0].Username, result[0].Username);
        Assert.Equal(employees[0].Email, result[0].Email);
        Assert.Equal(employees[0].HoursPerWeek, result[0].HoursPerWeek);
        Assert.Equal(employees[0].Role, result[0].Role);
        Assert.Equal(employees[0].EmployeeStatus, result[0].EmployeeStatus);
        
        Assert.Equal(employees[1].Id, result[1].Id);
        Assert.Equal(employees[1].Name, result[1].Name);
        Assert.Equal(employees[1].Username, result[1].Username);
        Assert.Equal(employees[1].Email, result[1].Email);
        Assert.Equal(employees[1].HoursPerWeek, result[1].HoursPerWeek);
        Assert.Equal(employees[1].Role, result[1].Role);
        Assert.Equal(employees[1].EmployeeStatus, result[1].EmployeeStatus);
    }
}