using MediatR;

namespace TimesheetApp.Application.WorkingHour.Commands.DeleteWorkingHour
{
    public sealed record DeleteWorkingHourCommand(Guid Id) : IRequest;
}