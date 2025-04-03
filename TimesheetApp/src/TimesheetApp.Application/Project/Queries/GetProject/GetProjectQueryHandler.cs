using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.ProjectRepository;

namespace TimesheetApp.Application.Project.Queries.GetProject;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, GetProjectQueryResponse>
{
    private readonly IProjectRepository _projectRepository;
    
    public GetProjectQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<GetProjectQueryResponse> Handle(GetProjectQuery query,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.Get(query.Id, cancellationToken);
        if (project == null) throw new NoDataFoundException("Project not found");
        return project.ToResponse();
    }
}