using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Extensions;

public static class CategoryExtensions
{
    public static TimesheetApp.Domain.Models.CategoryOverview ToOverview(this Category entity)
    {
        return new TimesheetApp.Domain.Models.CategoryOverview
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}