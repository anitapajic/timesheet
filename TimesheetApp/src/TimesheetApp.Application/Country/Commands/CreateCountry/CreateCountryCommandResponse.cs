namespace TimesheetApp.Application.Country.Commands.CreateCountry;

public sealed record CreateCountryCommandResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}