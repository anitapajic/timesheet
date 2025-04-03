using MediatR;
using TimesheetApp.Core.Repositories.EmployeeRepository;

namespace TimesheetApp.Application.Employee.Queries.ListEmployees;

public class ListEmployeesQueryHandler : IRequestHandler<ListEmployeesQuery,  List<ListEmployeesQueryResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public ListEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    public async Task<List<ListEmployeesQueryResponse>> Handle(ListEmployeesQuery query, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetAll(cancellationToken);
        return employees.ToResponseList();
    }
}