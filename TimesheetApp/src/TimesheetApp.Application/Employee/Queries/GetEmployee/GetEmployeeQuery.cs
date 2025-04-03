using MediatR;

namespace TimesheetApp.Application.Employee.Queries.GetEmployee
{
    public sealed record GetEmployeeQuery(Guid Id) : IRequest<GetEmployeeQueryResponse>;
}