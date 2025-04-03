using MediatR;

namespace TimesheetApp.Application.Project.Queries.GetProject
{
    public sealed record GetProjectQuery(Guid Id) : IRequest<GetProjectQueryResponse>;
}