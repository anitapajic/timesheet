using TimesheetApp.Domain.Base;

namespace TimesheetApp.Domain.Models;

public class Category : BaseModel
{
    public required string Name { get; set; }
}