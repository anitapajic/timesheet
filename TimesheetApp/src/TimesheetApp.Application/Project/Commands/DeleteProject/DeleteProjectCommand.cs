using MediatR;

namespace TimesheetApp.Application.Project.Commands.DeleteProject
{
    public sealed record DeleteProjectCommand(Guid Id) : IRequest;
}