using TimesheetApp.Application.Category.Queries.ListCategories;
using Xunit;

namespace UnitTest.Application.Category.Queries.ListCategories;

public class ListCategoriesQueryMapperTest
{
    [Fact]
    public void ToResponse_ShouldMapCategoryToResponse()
    {
        var category = new TimesheetApp.Domain.Models.Category
        {
            Id = Guid.NewGuid(),
            Name = "TestCategory"
        };
            
        var response = category.ToResponse();
            
        Assert.NotNull(response);
        Assert.Equal(category.Id, response.Id);
        Assert.Equal(category.Name, response.Name);
    }

    [Fact]
    public void ToResponseList_ShouldMapListOfCategoriesToResponseList()
    {
        var categories = new List<TimesheetApp.Domain.Models.Category>
        {
            new() { Id = Guid.NewGuid(), Name = "Category1" },
            new() { Id = Guid.NewGuid(), Name = "Category2" }
        };
            
        var responseList = categories.ToResponseList();
            
        Assert.NotNull(responseList);
        Assert.Equal(categories.Count, responseList.Count);
        Assert.Equal(categories[0].Id, responseList[0].Id);
        Assert.Equal(categories[0].Name, responseList[0].Name);
        Assert.Equal(categories[1].Id, responseList[1].Id);
        Assert.Equal(categories[1].Name, responseList[1].Name);
    }
}