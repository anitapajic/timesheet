using TimesheetApp.Application.Common.Utils;
using TimesheetApp.Application.Employee.Commands.CreateEmployee;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandMapperTest
{
    [Fact]
    public void ToDomain_ShouldMapCommandToDomainModel()
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
        
        command.Password = PasswordHasher.HashPassword(command.Password);
        
        var domainModel = command.ToDomain();
        
        Assert.NotNull(domainModel);
        Assert.Equal(command.Name, domainModel.Name);
        Assert.Equal(command.Username, domainModel.Username);
        Assert.Equal(command.Email, domainModel.Email);
        Assert.Equal(command.Password, domainModel.Password);
        Assert.Equal(command.HoursPerWeek, domainModel.HoursPerWeek);
        Assert.Equal(command.Role, domainModel.Role);
        Assert.Equal(command.EmployeeStatus, domainModel.EmployeeStatus);
    }

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