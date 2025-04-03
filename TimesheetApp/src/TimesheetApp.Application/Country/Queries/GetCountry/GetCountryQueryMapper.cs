namespace TimesheetApp.Application.Country.Queries.GetCountry;

public static class GetCountryQueryMapper
{
    public static GetCountryQueryResponse ToResponse(this Domain.Models.Country country)
    {
        return new GetCountryQueryResponse
        {
            Id = country.Id,
            Name = country.Name
        };
    }
}