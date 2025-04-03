using MediatR;

namespace TimesheetApp.Application.Employee.Commands.DeleteEmployee
{
    public sealed record DeleteEmployeeCommand(Guid Id) : IRequest;
}