using System.Text.Json.Serialization;
using MediatR;
using TimesheetApp.Domain.Models.Enums;

namespace TimesheetApp.Application.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommand : IRequest<CreateEmployeeCommandResponse>
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
}