using NSubstitute;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Country.Queries.GetCountry;
using TimesheetApp.Core.Repositories.CountryRepository;
using Xunit;

namespace UnitTest.Application.Country.Queries.GetCountry;

public class GetCountryQueryHandlerTest
{
    
    [Fact]
    public async Task Handle_ShouldReturnCountry_WhenExists()
    {
        var id = Guid.NewGuid();
        var query = new GetCountryQuery(id);
        var country = new TimesheetApp.Domain.Models.Country { Id = id, Name = "TestCountry" };
        
        var testContext = new TestContext()
            .WithCountryRepositoryGetSetup(query.Id, country);
        
        var handler = testContext.CreateHandler;
        var result = await handler.Handle(query, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(country.Id, result.Id);
        Assert.Equal(country.Name, result.Name);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowException_WhenCountryNotFound()
    {
        var id = Guid.NewGuid();
        var query = new GetCountryQuery(id);
        
        var testContext = new TestContext()
            .WithCountryRepositoryGetSetup(query.Id, null);
        
        var handler = testContext.CreateHandler;
        
        await Assert.ThrowsAsync<NoDataFoundException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }
    
    internal class TestContext
    {
        private readonly ICountryRepository _countryRepository;
        
        public TestContext()
        {
            _countryRepository = Substitute.For<ICountryRepository>();
        }
        
        public TestContext WithCountryRepositoryGetSetup(Guid id, TimesheetApp.Domain.Models.Country country)
        {
            _countryRepository.Get(
                id, Arg.Any<CancellationToken>())
                .Returns(country);
            return this;
        }
        
        public GetCountryQueryHandler CreateHandler => new(_countryRepository);
    }

}