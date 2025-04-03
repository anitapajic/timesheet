using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Extensions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Domain.Models.Enums;

namespace TimesheetApp.Application.Project.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectCommandResponse>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, 
                                       IEmployeeRepository employeeRepository,
                                       IClientRepository clientRepository,
                                       IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _employeeRepository = employeeRepository;
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(command.LeadId, cancellationToken)
                       ?? throw new NoDataFoundException("Employee not found");
        var client = await _clientRepository.Get(command.ClientId, cancellationToken)
                     ?? throw new NoDataFoundException("Client not found");
        
        var domainProject = command.ToDomain();
        domainProject.Client = client.ToClientOverview();
        domainProject.Lead = employee.ToEmployeeOverview();
        domainProject.ProjectStatus = ProjectStatus.Active;
            
        var createdProject = await _projectRepository.CreateAsync(domainProject, cancellationToken);

        await _unitOfWork.Save(cancellationToken);
        return createdProject.ToResponse();
    }
}