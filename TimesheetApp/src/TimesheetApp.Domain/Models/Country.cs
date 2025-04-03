using TimesheetApp.Domain.Base;

namespace TimesheetApp.Domain.Models;

public class Country : BaseModel
{
    public required string Name { get; set; }
}