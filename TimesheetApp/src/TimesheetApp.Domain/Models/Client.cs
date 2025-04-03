using TimesheetApp.Domain.Base;

namespace TimesheetApp.Domain.Models;

public class Client : BaseModel
{
    public required string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public CountryOverview Country { get; set; }
}