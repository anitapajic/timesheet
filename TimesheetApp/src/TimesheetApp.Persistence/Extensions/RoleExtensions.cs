using RoleEntity = Timesheet.Infrastructure.Entities.Enums.Role;
using RoleDomain = TimesheetApp.Domain.Models.Enums.Role;

namespace Timesheet.Infrastructure.Extensions;

public static class RoleExtensions
{
    public static RoleEntity ToEntity(this RoleDomain domainStatus)
    {
        return domainStatus switch
        {
            RoleDomain.Admin => RoleEntity.Admin,
            RoleDomain.Worker => RoleEntity.Worker,
            _ => throw new ArgumentOutOfRangeException(nameof(domainStatus), $"Unknown domain status: {domainStatus}")
        };
    }

    public static RoleDomain ToDomain(this RoleEntity entityStatus)
    {
        return entityStatus switch
        {
            RoleEntity.Admin => RoleDomain.Admin,
            RoleEntity.Worker => RoleDomain.Worker,
            _ => throw new ArgumentOutOfRangeException(nameof(entityStatus), $"Unknown entity status: {entityStatus}")
        };
    }
}