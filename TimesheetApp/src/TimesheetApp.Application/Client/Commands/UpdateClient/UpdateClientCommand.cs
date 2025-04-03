using MediatR;

namespace TimesheetApp.Application.Client.Commands.UpdateClient;

public class UpdateClientCommand : IRequest<UpdateClientCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string CountryName { get; set; }
}