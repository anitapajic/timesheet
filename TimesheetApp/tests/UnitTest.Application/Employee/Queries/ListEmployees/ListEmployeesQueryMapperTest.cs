using TimesheetApp.Application.Employee.Queries.ListEmployees;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Queries.ListEmployees;

public class ListEmployeesQueryMapperTest
{
    [Fact]
    public void ToResponse_ShouldMapDomainModelToResponse()
    {
        var domainModel = new TimesheetApp.Domain.Models.Employee
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
        
        var response = domainModel.ToResponse();
        
        Assert.NotNull(response);
        Assert.Equal(domainModel.Id, response.Id);
        Assert.Equal(domainModel.Name, response.Name);
        Assert.Equal(domainModel.Username, response.Username);
        Assert.Equal(domainModel.Email, response.Email);
        Assert.Equal(domainModel.HoursPerWeek, response.HoursPerWeek);
        Assert.Equal(domainModel.Role, response.Role);
        Assert.Equal(domainModel.EmployeeStatus, response.EmployeeStatus);
    }
    [Fact]
    public void ToResponseList_ShouldMapListOfEmployeesToResponseList()
    {
        var employees = new List<TimesheetApp.Domain.Models.Employee>
        {
            new() { Id = Guid.NewGuid(), Name = "name1", Username = "username1", Email = "email1@mail.com", Password = "123456", HoursPerWeek = 40, Role = Role.Worker, EmployeeStatus = EmployeeStatus.Active},
            new() {  Id = Guid.NewGuid(), Name = "name2", Username = "username2", Email = "email2@mail.com", Password = "123456", HoursPerWeek = 40,  Role = Role.Worker, EmployeeStatus = EmployeeStatus.Active}
        };
            
        var responseList = employees.ToResponseList();
            
        Assert.NotNull(responseList);
        Assert.Equal(employees.Count, responseList.Count);
        Assert.Equal(employees[0].Id, responseList[0].Id);
        Assert.Equal(employees[0].Name, responseList[0].Name);
        Assert.Equal(employees[0].Username, responseList[0].Username);
        Assert.Equal(employees[0].Email, responseList[0].Email);
        Assert.Equal(employees[0].HoursPerWeek, responseList[0].HoursPerWeek);
        Assert.Equal(employees[0].Role, responseList[0].Role);
        Assert.Equal(employees[0].EmployeeStatus, responseList[0].EmployeeStatus);
        
        Assert.Equal(employees[1].Id, responseList[1].Id);
        Assert.Equal(employees[1].Name, responseList[1].Name);
        Assert.Equal(employees[1].Username, responseList[1].Username);
        Assert.Equal(employees[1].Email, responseList[1].Email);
        Assert.Equal(employees[1].HoursPerWeek, responseList[1].HoursPerWeek);
        Assert.Equal(employees[1].Role, responseList[1].Role);
        Assert.Equal(employees[1].EmployeeStatus, responseList[1].EmployeeStatus);

    }
}