using MediatR;

namespace TimesheetApp.Application.Project.Commands.CreateProject;

public class CreateProjectCommand : IRequest<CreateProjectCommandResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ClientId { get; set; }
    public Guid LeadId { get; set; }
}