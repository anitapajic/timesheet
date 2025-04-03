using MediatR;

namespace TimesheetApp.Application.WorkingHour.Queries.ListWorkingHours
{
    public sealed record ListWorkingHoursQuery : IRequest<List<ListWorkingHoursQueryResponse>>;
}