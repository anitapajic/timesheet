using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Project.Queries.GetProject;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Domain.Models.Enums;
using Xunit;

namespace UnitTest.Application.Project.Queries.GetProject;

public class GetProjectQueryHandlerTest
{
    [Fact]
    public async Task Handle_ShouldReturnProject_WhenExists()
    {
        var testContext = new TestContext();
        var id = Guid.NewGuid();
        var query = new GetProjectQuery(id);
        var employee = TestContext.GetProject(query.Id);
            
        testContext
            .WithProjectRepositoryGetSetUp(query.Id, employee);
        
        var handler = testContext.CreateHandler;
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(employee.Id, result.Id);
        Assert.Equal(employee.Name, result.Name);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenProjectNotFound()
    {
        var testContext = new TestContext();
        var id = Guid.NewGuid();
        var query = new GetProjectQuery(id);
        
        testContext
            .WithProjectRepositoryGetSetUp(query.Id, null);
        
        var handler = testContext.CreateHandler;

        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }
    
     internal class TestContext
    {
        private readonly Mock<IProjectRepository> _projectRepository;
        public TestContext()
        {
            _projectRepository = new Mock<IProjectRepository>();
        }
        
        public static TimesheetApp.Domain.Models.Project GetProject(Guid id)
        {
            return new TimesheetApp.Domain.Models.Project
            {
                Id = id,
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
        
        public TestContext WithProjectRepositoryGetSetUp(Guid id, TimesheetApp.Domain.Models.Project project)
        {
            _projectRepository.Setup(repo => repo.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(project);
            return this;
        }
        
  
        public GetProjectQueryHandler CreateHandler => new(_projectRepository.Object);
    }
}