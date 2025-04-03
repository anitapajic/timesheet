using TimesheetApp.Application.Project.Commands.UpdateProject;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Project.Commands.UpdateProject;

public class UpdateProjectCommmandMapperTest
{
    [Fact]
    public void ToDomain_ShouldMapCommandToDomainModel()
    {
        var command = TestContext.GetCommand();
        
        var domainModel = command.ToDomain();
        
        Assert.NotNull(domainModel);
        Assert.Equal(command.Id, domainModel.Id);
        Assert.Equal(command.Name, domainModel.Name);
        Assert.Equal(command.Description, domainModel.Description);
        Assert.Equal(command.ClientId, domainModel.ClientId);
        Assert.Equal(command.LeadId, domainModel.LeadId);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainModelToResponse()
    {
        var domainModel = TestContext.GetProject();
        var response = UpdateProjectCommandMapper.ToResponse(domainModel);
        
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
        public static UpdateProjectCommand GetCommand()
        {
            return new UpdateProjectCommand
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Description = "description",
                ClientId = Guid.NewGuid(),
                LeadId = Guid.NewGuid(),
            };
        }
        
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