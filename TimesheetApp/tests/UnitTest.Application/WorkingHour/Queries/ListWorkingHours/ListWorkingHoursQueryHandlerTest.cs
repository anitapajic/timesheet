using NSubstitute;
using TimesheetApp.Application.WorkingHour.Queries.ListWorkingHours;
using TimesheetApp.Core.Repositories.WorkingHourRepository;
using TimesheetApp.Domain.Models;
using Xunit;

namespace UnitTest.Application.WorkingHour.Queries.ListWorkingHours;

public class ListWorkingHoursQueryHandlerTest
{
    private readonly IWorkingHourRepository _workingHourRepository;
    private readonly ListWorkingHoursQueryHandler _handler;
    
    public ListWorkingHoursQueryHandlerTest()
    {
        _workingHourRepository = Substitute.For<IWorkingHourRepository>();
        _handler = new ListWorkingHoursQueryHandler(_workingHourRepository);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnListOfWorkingHours()
    {
        var query = new ListWorkingHoursQuery();
        var workingHours = TestContext.GetWorkingHourList();
            
        _workingHourRepository.GetAll(Arg.Any<CancellationToken>()).Returns(workingHours);
            
        var responseList = await _handler.Handle(query, CancellationToken.None);
            
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