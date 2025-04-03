using TimesheetApp.Domain.Models;

namespace TimesheetApp.Application.Extensions;

public static class EmployeeExtensions
{
    public static EmployeeOverview ToEmployeeOverview(this Domain.Models.Employee employee)
        => new EmployeeOverview
        {
            Id = employee.Id,
            Name = employee.Name
        };
}