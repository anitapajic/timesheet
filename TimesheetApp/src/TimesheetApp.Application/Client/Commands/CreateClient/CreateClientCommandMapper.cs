namespace TimesheetApp.Application.Client.Commands.CreateClient;

public static class CreateClientCommandMapper
{
    public static Domain.Models.Client ToDomain(this CreateClientCommand command)
    {
        return new Domain.Models.Client
        {
            Name = command.Name,
            Address = command.Address,
            City = command.City,
            PostalCode = command.PostalCode,
        };
    }

    public static CreateClientCommandResponse ToResponse(this Domain.Models.Client client)
    {
        return new CreateClientCommandResponse
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