using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Common.Utils;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.EmployeeRepository;

namespace TimesheetApp.Application.Employee.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeCommandResponse>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var username = await _employeeRepository.ExistsByUsernameAsync(command.Username);
        if (username)
        {
            throw new EntityAlreadyExistsException("Employee with username '" + command.Username + "' already exists.");
        }
        
        var email = await _employeeRepository.ExistsByEmailAsync(command.Email);
        if (email)
        {
            throw new EntityAlreadyExistsException("Employee with email '" + command.Email + "' already exists.");
        }

        command.Password = PasswordHasher.HashPassword(command.Password);
            
        var domainEmployee = command.ToDomain();
            
        var createdEmployee = await _employeeRepository.CreateAsync(domainEmployee, cancellationToken);

        await _unitOfWork.Save(cancellationToken);
        return createdEmployee.ToResponse();
    }
}