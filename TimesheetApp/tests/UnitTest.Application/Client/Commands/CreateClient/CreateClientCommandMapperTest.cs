using TimesheetApp.Application.Client.Commands.CreateClient;
using Xunit;

namespace UnitTest.Application.Client.Commands.CreateClient;

public class CreateClientCommandMapperTest
{
    [Fact]
    public void ToDomain_ShouldMapCommandToDomainModel()
    {
        var command = new CreateClientCommand { Name = "TestCountry", Address = "Address", City = "City", PostalCode = "10000"
            , CountryName = "TestCountry" };
        
        var domainModel = command.ToDomain();
        
        Assert.NotNull(domainModel);
        Assert.Equal(command.Name, domainModel.Name);
        Assert.Equal(command.Address, domainModel.Address);
        Assert.Equal(command.City, domainModel.City);
        Assert.Equal(command.PostalCode, domainModel.PostalCode);
    }

    [Fact]
    public void ToResponse_ShouldMapDomainModelToResponse()
    {
        var domainModel = new TimesheetApp.Domain.Models.Client
        {
            Id = Guid.NewGuid(),
            Name = "TestClient",
            Address = "Address",
            City = "City",
            PostalCode = "10000",
            Country = new TimesheetApp.Domain.Models.CountryOverview { Name = "TestCountry" }
        };
        
        var response = domainModel.ToResponse();
        
        Assert.NotNull(response);
        Assert.Equal(domainModel.Id, response.Id);
        Assert.Equal(domainModel.Name, response.Name);
        Assert.Equal(domainModel.Address, response.Address);
        Assert.Equal(domainModel.City, response.City);
        Assert.Equal(domainModel.PostalCode, response.PostalCode);
        Assert.Equal(domainModel.Country.Name, response.CountryName);
    }
}