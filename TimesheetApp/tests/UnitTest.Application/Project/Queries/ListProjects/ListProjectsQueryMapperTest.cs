using TimesheetApp.Application.Project.Queries.ListProjects;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Project.Queries.ListProjects;

public class ListProjectsQueryMapperTest
{
    [Fact]
    public void ToResponse_ShouldMapDomainModelToResponse()
    {
        var domainModel = TestContext.GetProject();
        
        var response = domainModel.ToResponse();
        
        Assert.NotNull(response);
        Assert.Equal(domainModel.Id, response.Id);
        Assert.Equal(domainModel.Name, response.Name);
        Assert.Equal(domainModel.Description, response.Description);
        Assert.Equal(domainModel.ClientId, response.ClientId);
        Assert.Equal(domainModel.Client.Name, response.ClientName);
        Assert.Equal(domainModel.LeadId, response.LeadId);
        Assert.Equal(domainModel.Lead.Name, response.LeadName);
        Assert.Equal(domainModel.ProjectStatus, response.ProjectStatus);
    }
    [Fact]
    public void ToResponseList_ShouldMapListOfEmployeesToResponseList()
    {
        var projects = TestContext.GetProjectList();
            
        var responseList = projects.ToResponseList();
            
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
        public static TimesheetApp.Domain.Models.Project GetProject()
        {
            return new TimesheetApp.Domain.Models.Project
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Description = "description",
                ClientId = Guid.NewGuid(),
                Client = new TimesheetApp.Domain.Models.ClientOverview
                {
                    Id = Guid.NewGuid(),
                    Name = "name",
                },
                LeadId = Guid.NewGuid(),
                Lead = new TimesheetApp.Domain.Models.EmployeeOverview
                {
                    Id = Guid.NewGuid(),
                    Name = "name",
                },
                ProjectStatus = ProjectStatus.Active
            };
        }
        
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
                        Name = "Lead 1",
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