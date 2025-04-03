namespace TimesheetApp.Application.Country.Commands.UpdateCountry;

public static class UpdateCountryCommandMapper 
{
    public static Domain.Models.Country ToDomain(this UpdateCountryCommand command)
    {
        return new Domain.Models.Country
        {
            Id = command.Id,
            Name = command.Name
        };
    }

    public static UpdateCountryCommandResponse ToResponse(this Domain.Models.Country country)
    {
        return new UpdateCountryCommandResponse
        {
            Id = country.Id,
            Name = country.Name
        };
    }
}