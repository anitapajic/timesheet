using NSubstitute;
using TimesheetApp.Application.Category.Queries.ListCategories;
using TimesheetApp.Core.Repositories.CategoryRepository;
using Xunit;

namespace UnitTest.Application.Category.Queries.ListCategories;

public class ListCategoriesQueryHandlerTest
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ListCategoriesQueryHandler _handler;
    
    public ListCategoriesQueryHandlerTest()
    {
        _categoryRepository = Substitute.For<ICategoryRepository>();
        _handler = new ListCategoriesQueryHandler(_categoryRepository);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnListOfCategories()
    {
        var query = new ListCategoriesQuery();
        var categories = new List<TimesheetApp.Domain.Models.Category>
        {
            new() { Id = Guid.NewGuid(), Name = "Category1" },
            new() { Id = Guid.NewGuid(), Name = "Category2" }
        };
            
        _categoryRepository.GetAll(Arg.Any<CancellationToken>()).Returns(categories);
            
        var result = await _handler.Handle(query, CancellationToken.None);
            
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Category1", result[0].Name);
        Assert.Equal("Category2", result[1].Name);
    }
}