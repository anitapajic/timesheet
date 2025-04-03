using System.Text.Json.Serialization;
using Timesheet.Infrastructure.Entities.Base;
using Timesheet.Infrastructure.Entities.Enums;

namespace Timesheet.Infrastructure.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int HoursPerWeek { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EmployeeStatus EmployeeStatus { get; set; }
    public List<Project> Projects { get; set; }
}