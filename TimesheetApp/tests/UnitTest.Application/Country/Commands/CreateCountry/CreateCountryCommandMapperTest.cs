using TimesheetApp.Application.Country.Commands.CreateCountry;
using Xunit;

namespace UnitTest.Application.Country.Commands.CreateCountry;

public class CreateCountryCommandMapperTest
{
    [Fact]
    public void ToDomain_ShouldMapCommandToDomainModel()
    {
        var command = new CreateCountryCommand("TestCountry");
        
        var domainModel = command.ToDomain();
        
        Assert.NotNull(domainModel);
        Assert.Equal(command.Name, domainModel.Name);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainModelToResponse()
    {
        var domainModel = new TimesheetApp.Domain.Models.Country
        {
            Id = Guid.NewGuid(),
            Name = "TestCountry"
        };
        
        var response = domainModel.ToResponse();
        
        Assert.NotNull(response);
        Assert.Equal(domainModel.Id, response.Id);
        Assert.Equal(domainModel.Name, response.Name);
    }
}