namespace TimesheetApp.Application.Employee.Commands.UpdateEmployee;

public static class UpdateEmployeeCommandMapper
{
    public static Domain.Models.Employee ToDomain(this UpdateEmployeeCommand command)
    {
        return new Domain.Models.Employee
        {
            Id = command.Id,
            Name = command.Name,
            Username = command.Username,
            Email = command.Email,
            Password = command.Password,
            HoursPerWeek = command.HoursPerWeek,
            Role = command.Role,
            EmployeeStatus = command.EmployeeStatus,
        };
    }

    public static UpdateEmployeeCommandResponse ToResponse(this Domain.Models.Employee employee)
    {
        return new UpdateEmployeeCommandResponse
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