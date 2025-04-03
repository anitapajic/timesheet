using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Extensions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Core.Repositories.WorkingHourRepository;

namespace TimesheetApp.Application.WorkingHour.Commands.CreateWorkingHour;

public class CreateWorkingHourCommandHandler : IRequestHandler<CreateWorkingHourCommand, CreateWorkingHourCommandResponse>
{
    private IWorkingHourRepository _workingHourRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateWorkingHourCommandHandler(
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
    
    public async Task<CreateWorkingHourCommandResponse> Handle(CreateWorkingHourCommand command,
        CancellationToken cancellationToken)
    {
        var (category, client, project, employee) = await Get(command, cancellationToken);
        
        CheckData(client, project, employee);
        CheckDate(command.Date);
        
        var domainWorkingHour = command.ToDomain();
        domainWorkingHour.Employee = employee.ToEmployeeOverview();
        domainWorkingHour.Client = client.ToClientOverview();
        domainWorkingHour.Project = project.ToProjectOverview();
        domainWorkingHour.Category = category.ToCategoryOverview();
            
        var createdWorkingHour = await _workingHourRepository.CreateAsync(domainWorkingHour, cancellationToken);

        await _unitOfWork.Save(cancellationToken);
        return createdWorkingHour.ToResponse();
    }

    private async Task<(Domain.Models.Category category, Domain.Models.Client client, Domain.Models.Project project, Domain.Models.Employee employee)> 
        Get(CreateWorkingHourCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(command.EmployeeId, cancellationToken)
                       ?? throw new NoDataFoundException("Employee not found");
        var client = await _clientRepository.Get(command.ClientId, cancellationToken)
                     ?? throw new NoDataFoundException("Client not found");
        var project = await _projectRepository.Get(command.ProjectId, cancellationToken)
                      ?? throw new NoDataFoundException("Project not found");
        var category = await _categoryRepository.Get(command.CategoryId, cancellationToken)
                       ?? throw new NoDataFoundException("Category not found");

        return (category, client, project, employee);
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

