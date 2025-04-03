namespace TimesheetApp.Application.Client.Commands.UpdateClient;

public class UpdateClientCommandResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string CountryName { get; set; }
}