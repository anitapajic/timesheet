using Moq;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.WorkingHour.Queries.GetWorkingHour;
using TimesheetApp.Core.Repositories.WorkingHourRepository;
using TimesheetApp.Domain.Models;
using Xunit;

namespace UnitTest.Application.WorkingHour.Queries.GetWorkingHour;

public class GetWorkingHourQueryHandlerTest
{
    [Fact]
    public async Task Handle_ShouldReturnWorkingHour_WhenExists()
    {
        var testContext = new TestContext();
        var id = Guid.NewGuid();
        var query = new GetWorkingHourQuery(id);
        var workingHour = TestContext.GetWorkingHour(query.Id);
            
        testContext
            .WithWorkingHourRepositoryGetSetUp(query.Id, workingHour);
        
        var handler = testContext.CreateHandler;
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(workingHour.Id, result.Id);
        Assert.Equal(workingHour.Time, result.Time);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenWorkingHourNotFound()
    {
        var testContext = new TestContext();
        var id = Guid.NewGuid();
        var query = new GetWorkingHourQuery(id);
        
        testContext
            .WithWorkingHourRepositoryGetSetUp(query.Id, null!);
        
        var handler = testContext.CreateHandler;

        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }

    internal class TestContext
    {
        private readonly Mock<IWorkingHourRepository> _workingHourRepository;

        public TestContext()
        {
            _workingHourRepository = new Mock<IWorkingHourRepository>();
        }

        public static TimesheetApp.Domain.Models.WorkingHour GetWorkingHour(Guid id)
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

        public TestContext WithWorkingHourRepositoryGetSetUp(Guid id, TimesheetApp.Domain.Models.WorkingHour workingHour)
        {
            _workingHourRepository.Setup(repo => repo.Get(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workingHour);
            return this;
        }


        public GetWorkingHourQueryHandler CreateHandler => new(_workingHourRepository.Object);
    }
}