using System.Text.Json.Serialization;
using TimesheetApp.Domain.Models;
using TimesheetApp.Domain.Models.Enums;

namespace TimesheetApp.Application.Project.Commands.UpdateProject;

public class UpdateProjectCommandResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ClientId { get; set; }
    public string ClientName { get; set; }
    public Guid LeadId { get; set; }
    public string LeadName { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProjectStatus ProjectStatus { get; set; }
    public List<EmployeeOverview>? Employees { get; set; }
}