using TimesheetApp.Application.Project.Commands.UpdateProject;
using TimesheetApp.Application.Project.Queries.GetProject;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Project.Queries.GetProject;

public class GetProjectQueryMapperTest
{
    [Fact]
    public void ToResponse_ShouldMapDomainModelToResponse()
    {
        var domainModel = TestContext.GetProject();
        var response = GetProjectQueryMapper.ToResponse(domainModel);
        
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
    }
}