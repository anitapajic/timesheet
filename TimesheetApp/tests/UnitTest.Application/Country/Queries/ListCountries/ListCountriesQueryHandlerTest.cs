using NSubstitute;
using TimesheetApp.Application.Country.Queries.ListCountries;
using TimesheetApp.Core.Repositories.CountryRepository;
using Xunit;

namespace UnitTest.Application.Country.Queries.ListCountries;

public class ListCountriesQueryHandlerTest
{
    private readonly ICountryRepository _countryRepository;
    private readonly ListCountriesQueryHandler _handler;
    
    public ListCountriesQueryHandlerTest()
    {
        _countryRepository = Substitute.For<ICountryRepository>();
        _handler = new ListCountriesQueryHandler(_countryRepository);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnListOfCountries()
    {
        var query = new ListCountriesQuery();
        var countries = new List<TimesheetApp.Domain.Models.Country>
        {
            new() { Id = Guid.NewGuid(), Name = "Country1" },
            new() { Id = Guid.NewGuid(), Name = "Country2" }
        };
            
        _countryRepository.GetAll(Arg.Any<CancellationToken>()).Returns(countries);
            
        var result = await _handler.Handle(query, CancellationToken.None);
            
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Country1", result[0].Name);
        Assert.Equal("Country2", result[1].Name);
    }
    
}