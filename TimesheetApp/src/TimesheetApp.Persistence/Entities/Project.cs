using Timesheet.Infrastructure.Entities.Base;
using Timesheet.Infrastructure.Entities.Enums;

namespace Timesheet.Infrastructure.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
    
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    
    public Guid LeadId { get; set; }
    public Employee? Lead;
    
    public List<Employee>? Employees { get; set; }
}