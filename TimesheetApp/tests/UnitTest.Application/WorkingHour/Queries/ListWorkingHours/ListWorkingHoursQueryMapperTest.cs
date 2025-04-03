using TimesheetApp.Application.WorkingHour.Queries.ListWorkingHours;
using TimesheetApp.Domain.Models;
using Xunit;

namespace UnitTest.Application.WorkingHour.Queries.ListWorkingHours;

public class ListWorkingHoursQueryMapperTest
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
        Assert.Equal(domainModel.EmployeeId, response.EmployeeId);
        Assert.Equal(domainModel.Employee?.Name, response.EmployeeName);
        Assert.Equal(domainModel.ProjectId, response.ProjectId);
        Assert.Equal(domainModel.Project?.Name, response.ProjectName);
        Assert.Equal(domainModel.CategoryId, response.CategoryId);
        Assert.Equal(domainModel.Category?.Name, response.CategoryName);
        Assert.Equal(domainModel.Description, response.Description);
        Assert.Equal(domainModel.Time, response.Time);
        Assert.Equal(domainModel.Overtime, response.Overtime);
        Assert.Equal(domainModel.Date, response.Date);
    }
    [Fact]
    public void ToResponseList_ShouldMapListOfWorkingHoursToResponseList()
    {
        var workingHours = TestContext.GetWorkingHourList();
            
        var responseList = workingHours.ToResponseList();
            
        Assert.NotNull(responseList);
        Assert.Equal(workingHours.Count, responseList.Count);
        for (var i = 0; i < workingHours.Count; i++)
        {
            Assert.Equal(workingHours[i].Id, responseList[i].Id);
            Assert.Equal(workingHours[i].Description, responseList[i].Description);
            Assert.Equal(workingHours[i].ClientId, responseList[i].ClientId);
            Assert.Equal(workingHours[i].Client?.Name, responseList[i].ClientName);
            Assert.Equal(workingHours[i].EmployeeId, responseList[i].EmployeeId);
            Assert.Equal(workingHours[i].Employee?.Name, responseList[i].EmployeeName);
            Assert.Equal(workingHours[i].ProjectId, responseList[i].ProjectId);
            Assert.Equal(workingHours[i].Project?.Name, responseList[i].ProjectName);
            Assert.Equal(workingHours[i].CategoryId, responseList[i].CategoryId);
            Assert.Equal(workingHours[i].Category?.Name, responseList[i].CategoryName);
            Assert.Equal(workingHours[i].Description, responseList[i].Description);
            Assert.Equal(workingHours[i].Time, responseList[i].Time);
            Assert.Equal(workingHours[i].Overtime, responseList[i].Overtime);
            Assert.Equal(workingHours[i].Date, responseList[i].Date);
        }

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

        public static List<TimesheetApp.Domain.Models.WorkingHour> GetWorkingHourList()
        {
            return new List<TimesheetApp.Domain.Models.WorkingHour>
            {
                new()
                {
                    Description = "desc1",
                    ClientId = Guid.NewGuid(),
                    Client = new ClientOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "name1",
                    },
                    ProjectId = Guid.NewGuid(),
                    Project = new ProjectOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "name1",
                    },
                    CategoryId = Guid.NewGuid(),
                    Category = new CategoryOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "name1",
                    },
                    EmployeeId = Guid.NewGuid(),
                    Employee = new EmployeeOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "name1",
                    },
                    Time = 8,
                    Overtime = 0,
                    Date = DateTime.UtcNow
                },
                new()
                {
                    Description = "desc2",
                    ClientId = Guid.NewGuid(),
                    Client = new ClientOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "name2",
                    },
                    ProjectId = Guid.NewGuid(),
                    Project = new ProjectOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "name2",
                    },
                    CategoryId = Guid.NewGuid(),
                    Category = new CategoryOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "name2",
                    },
                    EmployeeId = Guid.NewGuid(),
                    Employee = new EmployeeOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "name2",
                    },
                    Time = 8,
                    Overtime = 0,
                    Date = DateTime.UtcNow
                }
            };
        }
    }
}