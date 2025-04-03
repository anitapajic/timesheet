namespace TimesheetApp.Application.Employee.Commands.CreateEmployee;

public static class CreateEmployeeCommandMapper
{
    public static Domain.Models.Employee ToDomain(this CreateEmployeeCommand command)
    {
        return new Domain.Models.Employee
        {
            Name = command.Name,
            Username = command.Username,
            Email = command.Email,
            Password = command.Password,
            HoursPerWeek = command.HoursPerWeek,
            Role = command.Role,
            EmployeeStatus = command.EmployeeStatus,
        };
    }

    public static CreateEmployeeCommandResponse ToResponse(this Domain.Models.Employee employee)
    {
        return new CreateEmployeeCommandResponse
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