using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.EmployeeRepository;

namespace TimesheetApp.Application.Employee.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(command.Id, cancellationToken);
        if (employee == null) throw new NoDataFoundException("Employee not found");
        await _employeeRepository.DeleteAsync(employee.Id, cancellationToken);
        await _unitOfWork.Save(cancellationToken);
    }
}