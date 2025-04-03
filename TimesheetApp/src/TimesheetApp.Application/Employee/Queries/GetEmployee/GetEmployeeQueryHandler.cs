using MediatR;
using TimesheetApp.Application.Common.Exceptions;
using TimesheetApp.Core.Repositories.EmployeeRepository;

namespace TimesheetApp.Application.Employee.Queries.GetEmployee;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, GetEmployeeQueryResponse>
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    public async Task<GetEmployeeQueryResponse> Handle(GetEmployeeQuery query,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(query.Id, cancellationToken);
        if (employee == null) throw new NoDataFoundException("Employee not found");
        return employee.ToResponse();
    }
}