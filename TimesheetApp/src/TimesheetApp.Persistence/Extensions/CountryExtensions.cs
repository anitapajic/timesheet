using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Extensions;

public static class CountryExtensions
{
    public static TimesheetApp.Domain.Models.CountryOverview ToOverview(this Country entity)
    {
        return new TimesheetApp.Domain.Models.CountryOverview
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}