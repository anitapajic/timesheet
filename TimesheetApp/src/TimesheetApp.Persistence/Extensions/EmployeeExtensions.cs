using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Extensions;

public static class EmployeeExtensions
{
    public static TimesheetApp.Domain.Models.EmployeeOverview ToOverview(this Employee entity)
    {
        return new TimesheetApp.Domain.Models.EmployeeOverview
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}