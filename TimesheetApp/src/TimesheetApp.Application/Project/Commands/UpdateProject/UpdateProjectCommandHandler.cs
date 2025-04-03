using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Extensions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Project.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectCommandResponse>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository, 
        IEmployeeRepository employeeRepository,
        IClientRepository clientRepository,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _employeeRepository = employeeRepository;
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.Get(command.Id, cancellationToken)
                       ?? throw new NoDataFoundException("Project not found");
        var lead = await _employeeRepository.Get(command.LeadId, cancellationToken)
            ?? throw new NoDataFoundException("Lead employee not found");
        var client = await _clientRepository.Get(command.ClientId, cancellationToken)
            ?? throw new NoDataFoundException("Client not found");
        
        var employees = command.EmployeeIds != null
            ? await _employeeRepository.GetByIdsAsync(command.EmployeeIds, cancellationToken)
            : [];

        var employeesOverview = employees.Select(employee => employee.ToEmployeeOverview()).ToList();
        
        project.Name = command.Name;
        project.Description = command.Description;
        project.LeadId = command.LeadId;
        project.Lead = lead.ToEmployeeOverview();
        project.ClientId = command.ClientId;
        project.Client = client.ToClientOverview();
        project.ProjectStatus = command.ProjectStatus;
        project.Employees = employeesOverview;
        project.EmployeeIds = command.EmployeeIds;
        
        var updatedProject = await _projectRepository.UpdateAsync(project, cancellationToken);
        await _unitOfWork.Save(cancellationToken);
    
        return updatedProject.ToResponse();
    }
    
}