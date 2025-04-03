using TimesheetApp.Application.Category.Commands.CreateCategory;
using Xunit;

namespace UnitTest.Application.Category.Commands.CreateCategory;

public class CreateCategoryCommandMapperTest
{
    [Fact]
    public void ToDomain_ShouldMapCommandToDomainModel()
    {
        var command = new CreateCategoryCommand("TestCategory");
        
        var domainModel = command.ToDomain();
        
        Assert.NotNull(domainModel);
        Assert.Equal(command.Name, domainModel.Name);
    }

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