using TimesheetApp.Application.WorkingHour.Commands.CreateWorkingHour;
using TimesheetApp.Domain.Models;
using Xunit;

namespace UnitTest.Application.WorkingHour.Commands.CreateWorkingHour;

public class CreateWorkingHourCommandMapperTest
{
    
    [Fact]
    public void ToDomain_ShouldMapCommandToDomainModel()
    {
        var command = TestContext.GetCommand();
        
        var domainModel = command.ToDomain();
        
        Assert.NotNull(domainModel);
        Assert.Equal(command.Description, domainModel.Description);
        Assert.Equal(command.ClientId, domainModel.ClientId);
        Assert.Equal(command.ProjectId, domainModel.ProjectId);
        Assert.Equal(command.CategoryId, domainModel.CategoryId);
        Assert.Equal(command.EmployeeId, domainModel.EmployeeId);
        Assert.Equal(command.Time, domainModel.Time);
        Assert.Equal(command.Overtime, domainModel.Overtime);
        Assert.Equal(command.Date, domainModel.Date);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainModelToResponse()
    {
        var domainModel = TestContext.GetWorkingHour();
        
        var response = domainModel.ToResponse();
        
        Assert.NotNull(response);
        Assert.Equal(domainModel.Id, response.Id);
        Assert.Equal(domainModel.Description, response.Description);
        Assert.Equal(domainModel.ClientId, response.ClientId);
        Assert.Equal(domainModel.Client?.Name, response.ClientName);
        Assert.Equal(domainModel.ProjectId, response.ProjectId);
        Assert.Equal(domainModel.Project?.Name, response.ProjectName);
        Assert.Equal(domainModel.CategoryId, response.CategoryId);
        Assert.Equal(domainModel.Category?.Name, response.CategoryName);
        Assert.Equal(domainModel.EmployeeId, response.EmployeeId);
        Assert.Equal(domainModel.Employee?.Name, response.EmployeeName);
        Assert.Equal(domainModel.Date, response.Date);
        Assert.Equal(domainModel.Overtime, response.Overtime);
        Assert.Equal(domainModel.Time, response.Time);
    }
    
    internal class TestContext
    {
        public static CreateWorkingHourCommand GetCommand()
        {
            return new CreateWorkingHourCommand
            {
                Description = "desc",
                ClientId = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                EmployeeId = Guid.NewGuid(),
                Time = 8,
                Overtime = 0,
                Date = DateTime.UtcNow,
            };
        }

        public static TimesheetApp.Domain.Models.WorkingHour GetWorkingHour()
        {
            return new TimesheetApp.Domain.Models.WorkingHour
            {
                Description = "desc",
                ClientId = Guid.NewGuid(),
                Client = new ClientOverview
                {
                    Id = Guid.NewGuid(),
                    Name = "name",
                },
                ProjectId = Guid.NewGuid(),
                Project = new ProjectOverview
                {
                    Id = Guid.NewGuid(),
                    Name = "name",
                },
                CategoryId = Guid.NewGuid(),
                Category = new CategoryOverview
                {
                    Id = Guid.NewGuid(),
                    Name = "name",
                },
                EmployeeId = Guid.NewGuid(),
                Employee = new EmployeeOverview
                {
                    Id = Guid.NewGuid(),
                    Name = "name",
                },
                Time = 8,
                Overtime = 0,
                Date = DateTime.UtcNow
            };
        }
    }
}