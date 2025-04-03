using TimesheetApp.Application.WorkingHour.Queries.GetWorkingHour;
using TimesheetApp.Domain.Models;
using Xunit;

namespace UnitTest.Application.WorkingHour.Queries.GetWorkingHour;

public class GetWorkingHourQueryMapperTest
{
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