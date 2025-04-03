using MediatR;

namespace TimesheetApp.Application.Client.Commands.CreateClient;

public class CreateClientCommand : IRequest<CreateClientCommandResponse>
{
    public required string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string CountryName { get; set; }
}