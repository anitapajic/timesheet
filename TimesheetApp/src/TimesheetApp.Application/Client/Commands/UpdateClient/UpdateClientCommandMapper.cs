namespace TimesheetApp.Application.Client.Commands.UpdateClient;

public static class UpdateClientCommandMapper
{
    public static Domain.Models.Client ToDomain(this UpdateClientCommand command)
    {
        return new Domain.Models.Client
        {
            Id = command.Id,
            Name = command.Name,
            Address = command.Address,
            City = command.City,
            PostalCode = command.PostalCode,
        };
    }

    public static UpdateClientCommandResponse ToResponse(this Domain.Models.Client client)
    {
        return new UpdateClientCommandResponse
        {
            Id = client.Id,
            Name = client.Name,
            Address = client.Address,
            City = client.City,
            PostalCode = client.PostalCode,
            CountryName = client.Country?.Name ?? string.Empty
        };
    }
}