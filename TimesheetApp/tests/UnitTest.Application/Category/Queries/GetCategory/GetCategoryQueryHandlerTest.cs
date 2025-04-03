using NSubstitute;
using TimesheetApp.Application.Category.Queries.GetCategory;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.CategoryRepository;
using Xunit;

namespace UnitTest.Application.Category.Queries.GetCategory;

public class GetCategoryQueryHandlerTest
{
    
    [Fact]
    public async Task Handle_ShouldReturnCategory_WhenExists()
    {
        var id = Guid.NewGuid();
        var query = new GetCategoryQuery(id);
        var category = new TimesheetApp.Domain.Models.Category { Id = id, Name = "TestCategory" };
            
        var testContext = new TestContext()
            .WithCategoryRepositoryGetSetup(query.Id, category);
        
        var handler = testContext.CreateHandler;
        
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
        Assert.Equal(category.Name, result.Name);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenCategoryNotFound()
    {
        var id = Guid.NewGuid();
        var query = new GetCategoryQuery(id);
        
        var testContext = new TestContext()
            .WithCategoryRepositoryGetSetup(query.Id, null);
        
        var handler = testContext.CreateHandler;

        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }
    
    internal class TestContext
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public TestContext()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();
        }
        
        public TestContext WithCategoryRepositoryGetSetup(Guid id, TimesheetApp.Domain.Models.Category category)
        {
            _categoryRepository.Get(
                    id, Arg.Any<CancellationToken>())
                .Returns(category);
            return this;
        }
        
        public GetCategoryQueryHandler CreateHandler => new(_categoryRepository);
    }
}