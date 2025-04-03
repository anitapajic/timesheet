using TimesheetApp.Application.Category.Queries.GetCategory;
using Xunit;

namespace UnitTest.Application.Category.Queries.GetCategory;

public class GetCategoryQueryMapperTest
{
    [Fact]
    public void ToResponse_ShouldMapDomainModelToResponse()
    {
        var domainModel = new TimesheetApp.Domain.Models.Category
        {
            Id = Guid.NewGuid(),
            Name = "TestCategory"
        };
        
        var response = domainModel.ToResponse();
        
        Assert.NotNull(response);
        Assert.Equal(domainModel.Id, response.Id);
        Assert.Equal(domainModel.Name, response.Name);
    }
}