using MediatR;

namespace TimesheetApp.Application.Client.Commands.DeleteClient
{
    public sealed record DeleteClientCommand(Guid Id) : IRequest;
}