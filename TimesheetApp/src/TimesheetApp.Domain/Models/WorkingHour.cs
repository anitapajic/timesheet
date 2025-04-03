using TimesheetApp.Domain.Base;

namespace TimesheetApp.Domain.Models;

public class WorkingHour : BaseModel
{
    public Guid ClientId { get; set; }
    public ClientOverview? Client { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectOverview? Project { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryOverview? Category { get; set; }
    public string? Description { get; set; }
    public int Time { get; set; }
    public int? Overtime { get; set; }
    public Guid EmployeeId { get; set; }
    public EmployeeOverview? Employee { get; set; }
    public DateTime Date { get; set; }
}