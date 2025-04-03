using ProjectStatusEntity = Timesheet.Infrastructure.Entities.Enums.ProjectStatus;
using ProjectStatusDomain = TimesheetApp.Domain.Models.Enums.ProjectStatus;

namespace Timesheet.Infrastructure.Extensions;

public static class ProjectStatusExtensions
{
    public static ProjectStatusEntity ToEntity(this ProjectStatusDomain domainStatus)
    {
        return domainStatus switch
        {
            ProjectStatusDomain.Active => ProjectStatusEntity.Active,
            ProjectStatusDomain.Inactive => ProjectStatusEntity.Inactive,
            ProjectStatusDomain.Archived => ProjectStatusEntity.Archived,
            _ => throw new ArgumentOutOfRangeException(nameof(domainStatus), $"Unknown domain status: {domainStatus}")
        };
    }

    public static ProjectStatusDomain ToDomain(this ProjectStatusEntity entityStatus)
    {
        return entityStatus switch
        {
            ProjectStatusEntity.Active => ProjectStatusDomain.Active,
            ProjectStatusEntity.Inactive => ProjectStatusDomain.Inactive,
            ProjectStatusEntity.Archived => ProjectStatusDomain.Archived,
            _ => throw new ArgumentOutOfRangeException(nameof(entityStatus), $"Unknown entity status: {entityStatus}")
        };
    }
}