using TimesheetApp.Application.Country.Queries.GetCountry;
using Xunit;

namespace UnitTest.Application.Country.Queries.GetCountry;

public class GetCountryQueryMapperTest
{
    
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