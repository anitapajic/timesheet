using MediatR;

namespace TimesheetApp.Application.Project.Queries.ListProjects
{
    public sealed record ListProjectsQuery : IRequest<List<ListProjectsQueryResponse>>;
}