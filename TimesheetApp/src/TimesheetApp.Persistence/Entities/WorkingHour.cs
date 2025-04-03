using Timesheet.Infrastructure.Entities.Base;

namespace Timesheet.Infrastructure.Entities;

public class WorkingHour : BaseEntity
{
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public string? Description { get; set; }
    public int Time { get; set; }
    public int? Overtime { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public DateTime Date { get; set; }
}