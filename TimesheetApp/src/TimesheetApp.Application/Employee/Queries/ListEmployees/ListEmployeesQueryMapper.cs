namespace TimesheetApp.Application.Employee.Queries.ListEmployees;

public static class ListEmployeesQueryMapper
{
    public static ListEmployeesQueryResponse ToResponse(this Domain.Models.Employee employee)
    {
        return new ListEmployeesQueryResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            Username = employee.Username,
            Email = employee.Email,
            HoursPerWeek = employee.HoursPerWeek,
            Role = employee.Role,
            EmployeeStatus = employee.EmployeeStatus,
        };
    }
    
    public static List<ListEmployeesQueryResponse> ToResponseList(this List<Domain.Models.Employee> employees)
    {
        return employees.Select(c => c.ToResponse()).ToList();
    }
}