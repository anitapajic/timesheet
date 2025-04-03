using TimesheetApp.Domain.Base;
using TimesheetApp.Domain.Models.Enums;

namespace TimesheetApp.Domain.Models;

public class Project : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
    
    public Guid ClientId { get; set; }
    public ClientOverview Client { get; set; }
    
    public Guid LeadId { get; set; }
    public EmployeeOverview Lead;
    
    public List<Guid>? EmployeeIds { get; set; }
    public List<EmployeeOverview> Employees { get; set; }
}