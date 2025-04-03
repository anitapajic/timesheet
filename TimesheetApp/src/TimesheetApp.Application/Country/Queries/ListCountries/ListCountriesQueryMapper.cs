namespace TimesheetApp.Application.Country.Queries.ListCountries;

public static class ListCountriesQueryMapper 
{
    public static ListCountriesQueryResponse ToResponse(this Domain.Models.Country country)
    {
        return new ListCountriesQueryResponse
        {
            Id = country.Id,
            Name = country.Name
        };
    }
    
    public static List<ListCountriesQueryResponse> ToResponseList(this List<Domain.Models.Country> countries)
    {
        return countries.Select(c => c.ToResponse()).ToList();
    }
}