using MediatR;

namespace TimesheetApp.Application.Employee.Queries.ListEmployees
{
    public sealed record ListEmployeesQuery : IRequest<List<ListEmployeesQueryResponse>>;
}