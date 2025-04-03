using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Extensions;

public static class CountryExtensions
{
    public static CountryOverview ToCountryOverview(this Domain.Models.Country country)
        => new CountryOverview
        {
            Id = country.Id,
            Name = country.Name
        };
}