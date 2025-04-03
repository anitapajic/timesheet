namespace TimesheetApp.Application.Country.Queries.ListCountries;

public sealed record ListCountriesQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}