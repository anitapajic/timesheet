using System.Text.Json.Serialization;
using MediatR;
using TimesheetApp.Domain.Models.Enums;

namespace TimesheetApp.Application.Project.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<UpdateProjectCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ClientId { get; set; }
    public Guid LeadId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProjectStatus ProjectStatus { get; set; }
    public List<Guid>? EmployeeIds { get; set; }
}