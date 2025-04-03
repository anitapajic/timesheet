using System.Text.Json.Serialization;
using TimesheetApp.Domain.Base;
using TimesheetApp.Domain.Models.Enums;

namespace TimesheetApp.Domain.Models;

public class Employee : BaseModel
{
    public string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int HoursPerWeek { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EmployeeStatus EmployeeStatus { get; set; }
    public List<Project> Projects { get; set; }
}