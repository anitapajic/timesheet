using TimesheetApp.Application.Country.Queries.ListCountries;
using Xunit;

namespace UnitTest.Application.Country.Queries.ListCountries
{
    public class ListCountriesQueryMapperTest
    {
        [Fact]
        public void ToResponse_ShouldMapCountryToResponse()
        {
            var country = new TimesheetApp.Domain.Models.Country
            {
                Id = Guid.NewGuid(),
                Name = "TestCountry"
            };
            
            var response = country.ToResponse();
            
            Assert.NotNull(response);
            Assert.Equal(country.Id, response.Id);
            Assert.Equal(country.Name, response.Name);
        }

        [Fact]
        public void ToResponseList_ShouldMapListOfCountriesToResponseList()
        {
            var countries = new List<TimesheetApp.Domain.Models.Country>
            {
                new() { Id = Guid.NewGuid(), Name = "Country1" },
                new() { Id = Guid.NewGuid(), Name = "Country2" }
            };
            
            var responseList = countries.ToResponseList();
            
            Assert.NotNull(responseList);
            Assert.Equal(countries.Count, responseList.Count);
            Assert.Equal(countries[0].Id, responseList[0].Id);
            Assert.Equal(countries[0].Name, responseList[0].Name);
            Assert.Equal(countries[1].Id, responseList[1].Id);
            Assert.Equal(countries[1].Name, responseList[1].Name);
        }
    }
}