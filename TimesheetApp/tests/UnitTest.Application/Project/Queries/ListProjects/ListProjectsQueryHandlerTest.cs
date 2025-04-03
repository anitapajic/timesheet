using NSubstitute;
using TimesheetApp.Application.Project.Queries.ListProjects;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Project.Queries.ListProjects;

public class ListProjectsQueryHandlerTest
{
    private readonly IProjectRepository _projectRepository;
    private readonly ListProjectsQueryHandler _handler;
    
    public ListProjectsQueryHandlerTest()
    {
        _projectRepository = Substitute.For<IProjectRepository>();
        _handler = new ListProjectsQueryHandler(_projectRepository);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnListOfProjects()
    {
        var query = new ListProjectsQuery();
        var projects = TestContext.GetProjectList();
            
        _projectRepository.GetAll(Arg.Any<CancellationToken>()).Returns(projects);
            
        var responseList = await _handler.Handle(query, CancellationToken.None);
            
        Assert.NotNull(responseList);
        Assert.Equal(projects.Count, responseList.Count);
        for (var i = 0; i < projects.Count; i++)
        {
            Assert.Equal(projects[i].Id, responseList[i].Id);
            Assert.Equal(projects[i].Name, responseList[i].Name);
            Assert.Equal(projects[i].Description, responseList[i].Description);
            Assert.Equal(projects[i].ClientId, responseList[i].ClientId);
            Assert.Equal(projects[i].Client.Name, responseList[i].ClientName);
            Assert.Equal(projects[i].LeadId, responseList[i].LeadId);
            Assert.Equal(projects[i].Lead.Name, responseList[i].LeadName);
            Assert.Equal(projects[i].ProjectStatus, responseList[i].ProjectStatus);
        }
    }
    
    internal class TestContext
    {
          public static List<TimesheetApp.Domain.Models.Project> GetProjectList()
        {
            return new List<TimesheetApp.Domain.Models.Project>
            {
                 new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 1",
                    Description = "Description 1",
                    ClientId = Guid.NewGuid(),
                    Client = new TimesheetApp.Domain.Models.ClientOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "Client 1",
                    },
                    LeadId = Guid.NewGuid(),
                    Lead = new TimesheetApp.Domain.Models.EmployeeOverview
                    {
                        Id = Guid.NewGuid(),
                    },
                    ProjectStatus = ProjectStatus.Active
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 2",
                    Description = "Description 2",
                    ClientId = Guid.NewGuid(),
                    Client = new TimesheetApp.Domain.Models.ClientOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "Client 2",
                    },
                    LeadId = Guid.NewGuid(),
                    Lead = new TimesheetApp.Domain.Models.EmployeeOverview
                    {
                        Id = Guid.NewGuid(),
                        Name = "Lead 2",
                    },
                    ProjectStatus = ProjectStatus.Active
                }
            };
        }
    }
}