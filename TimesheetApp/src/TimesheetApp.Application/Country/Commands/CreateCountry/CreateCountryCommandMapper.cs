namespace TimesheetApp.Application.Country.Commands.CreateCountry;

public static class CreateCountryCommandMapper
{
    public static Domain.Models.Country ToDomain(this CreateCountryCommand command)
    {
        return new Domain.Models.Country
        {
            Name = command.Name
        };
    }

    public static CreateCountryCommandResponse ToResponse(this Domain.Models.Country country)
    {
        return new CreateCountryCommandResponse
        {
            Id = country.Id,
            Name = country.Name
        };
    }
}