using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Application.Common.Utils;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.EmployeeRepository;

namespace TimesheetApp.Application.Employee.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResponse>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(command.Id, cancellationToken)
                     ?? throw new NoDataFoundException("Employee not found");
        
        employee.Name = command.Name;
        employee.Username = command.Username;
        employee.Email = command.Email;
        employee.Password = PasswordHasher.HashPassword(command.Password);
        employee.HoursPerWeek = command.HoursPerWeek;
        employee.Role = command.Role;
        employee.EmployeeStatus = command.EmployeeStatus;
        
        var updatedEmployee = await _employeeRepository.UpdateAsync(employee, cancellationToken);
        await _unitOfWork.Save(cancellationToken);
    
        return updatedEmployee.ToResponse();
    }
    
}