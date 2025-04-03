using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Extensions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Core.Repositories.WorkingHourRepository;

namespace TimesheetApp.Application.WorkingHour.Commands.UpdateWorkingHour;

public class UpdateWorkingHourCommandHandler : IRequestHandler<UpdateWorkingHourCommand, UpdateWorkingHourCommandResponse>
{
    private IWorkingHourRepository _workingHourRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateWorkingHourCommandHandler(
        IWorkingHourRepository workingHourRepository,
        IProjectRepository projectRepository, 
        IEmployeeRepository employeeRepository,
        IClientRepository clientRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _workingHourRepository = workingHourRepository;
        _projectRepository = projectRepository;
        _employeeRepository = employeeRepository;
        _clientRepository = clientRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UpdateWorkingHourCommandResponse> Handle(UpdateWorkingHourCommand command,
        CancellationToken cancellationToken)
    {
        var (workingHour, category, client, project, employee) = await Get(command, cancellationToken);
        
        CheckData(client, project, employee);
        CheckDate(command.Date);
       
        workingHour.EmployeeId = command.EmployeeId;
        workingHour.Employee = employee.ToEmployeeOverview();
        workingHour.ClientId = command.ClientId;
        workingHour.Client = client.ToClientOverview();
        workingHour.ProjectId = command.ProjectId;
        workingHour.Project = project.ToProjectOverview();
        workingHour.CategoryId = command.CategoryId;
        workingHour.Category = category.ToCategoryOverview();
        workingHour.Description = command.Description;
        workingHour.Time = command.Time;
        workingHour.Overtime = command.Overtime;
        workingHour.Date = command.Date;
            
        var updatedWorkingHour = await _workingHourRepository.UpdateAsync(workingHour, cancellationToken);

        await _unitOfWork.Save(cancellationToken);
        return updatedWorkingHour.ToResponse();
    }
    
    private async Task<(Domain.Models.WorkingHour workingHour, Domain.Models.Category category, Domain.Models.Client client, Domain.Models.Project project, Domain.Models.Employee employee)> 
        Get(UpdateWorkingHourCommand command, CancellationToken cancellationToken)
    {
        var workingHour = await _workingHourRepository.Get(command.Id, cancellationToken)
                          ?? throw new NoDataFoundException("Working Hour not found");
        var employee = await _employeeRepository.Get(command.EmployeeId, cancellationToken)
                       ?? throw new NoDataFoundException("Employee not found");
        var client = await _clientRepository.Get(command.ClientId, cancellationToken)
                     ?? throw new NoDataFoundException("Client not found");
        var project = await _projectRepository.Get(command.ProjectId, cancellationToken)
                      ?? throw new NoDataFoundException("Project not found");
        var category = await _categoryRepository.Get(command.CategoryId, cancellationToken)
                       ?? throw new NoDataFoundException("Category not found");

        return (workingHour, category, client, project, employee);
    }

    private static void CheckData(Domain.Models.Client client, Domain.Models.Project project, Domain.Models.Employee employee)
    {
        if (client.Id != project.ClientId) throw new BadRequestException("Client does not belong to this project");
        
        var isLead = project.LeadId == employee.Id;
        var isTeamMember = project.Employees?.Any(e => e.Id == employee.Id) == true;
    
        if (!isLead && !isTeamMember)
            throw new BadRequestException("Employee does not belong to this project");
    }

    private static void CheckDate(DateTime date)
    {
        if (date > DateTime.Today) throw new DateOutOfRangeException("Date can not be in the future.");
    }
}