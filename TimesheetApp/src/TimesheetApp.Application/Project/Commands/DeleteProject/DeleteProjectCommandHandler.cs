using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ProjectRepository;

namespace TimesheetApp.Application.Project.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteProjectCommand command,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.Get(command.Id, cancellationToken);
        if (project == null) throw new NoDataFoundException("Project not found");
        await _projectRepository.DeleteAsync(project.Id, cancellationToken);
        await _unitOfWork.Save(cancellationToken);
    }
}