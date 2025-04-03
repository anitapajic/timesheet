using MediatR;
using TimesheetApp.Core.Repositories.ProjectRepository;

namespace TimesheetApp.Application.Project.Queries.ListProjects;

public class ListProjectsQueryHandler : IRequestHandler<ListProjectsQuery,  List<ListProjectsQueryResponse>>
{
    private readonly IProjectRepository _projectRepository;
    
    public ListProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<List<ListProjectsQueryResponse>> Handle(ListProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAll(cancellationToken);
        return projects.ToResponseList();
    }
}