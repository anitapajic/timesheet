using TimesheetApp.Application.Employee.Commands.UpdateEmployee;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Queries.GetEmployee;

public class GetEmployeeQueryMapperTest
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
}