using MediatR;

namespace TimesheetApp.Application.WorkingHour.Queries.GetWorkingHour
{
    public sealed record GetWorkingHourQuery(Guid Id) : IRequest<GetWorkingHourQueryResponse>;
}