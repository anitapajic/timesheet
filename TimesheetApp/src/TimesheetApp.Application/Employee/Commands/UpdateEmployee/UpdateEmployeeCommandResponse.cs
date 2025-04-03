using System.Text.Json.Serialization;
using TimesheetApp.Domain.Models.Enums;

namespace TimesheetApp.Application.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommandResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int HoursPerWeek { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EmployeeStatus EmployeeStatus { get; set; }
}