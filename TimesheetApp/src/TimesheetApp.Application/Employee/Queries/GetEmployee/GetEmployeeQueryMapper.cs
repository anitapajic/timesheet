namespace TimesheetApp.Application.Employee.Queries.GetEmployee;

public static class GetEmployeeQueryMapper
{
    public static GetEmployeeQueryResponse ToResponse(this Domain.Models.Employee employee)
    {
        return new GetEmployeeQueryResponse
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
}