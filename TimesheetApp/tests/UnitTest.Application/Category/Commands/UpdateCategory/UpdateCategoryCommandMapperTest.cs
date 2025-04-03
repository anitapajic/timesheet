using TimesheetApp.Application.Category.Commands.UpdateCategory;
using Xunit;

namespace UnitTest.Application.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandMapperTest
{
    [Fact]
    public void ToDomain_ShouldMapCommandToDomainModel()
    {
        var command = new UpdateCategoryCommand {Id = Guid.Empty, Name = "Category"};
        
        var domainModel = command.ToDomain();
        
        Assert.NotNull(domainModel);
        Assert.Equal(command.Name, domainModel.Name);
        Assert.Equal(command.Id, domainModel.Id);
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