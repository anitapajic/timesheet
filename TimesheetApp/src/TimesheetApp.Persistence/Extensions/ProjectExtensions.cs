using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Extensions;

public static class ProjectExtensions
{
    public static TimesheetApp.Domain.Models.ProjectOverview ToOverview(this Project entity)
    {
        return new TimesheetApp.Domain.Models.ProjectOverview
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}