using TimesheetApp.Application.Client.Queries.GetClient;
using Xunit;

namespace UnitTest.Application.Client.Queries.GetClient;

public class GetClientQueryMapperTest
{
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