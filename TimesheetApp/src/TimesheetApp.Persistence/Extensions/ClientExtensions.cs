using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Extensions;

public static class ClientExtensions
{
    public static TimesheetApp.Domain.Models.ClientOverview ToOverview(this Client entity)
    {
        return new TimesheetApp.Domain.Models.ClientOverview
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}