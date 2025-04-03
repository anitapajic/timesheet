using EmployeeStatusEntity = Timesheet.Infrastructure.Entities.Enums.EmployeeStatus;
using EmployeeStatusDomain = TimesheetApp.Domain.Models.Enums.EmployeeStatus;

namespace Timesheet.Infrastructure.Extensions;

public static class EmployeeStatusExtensions
{
    public static EmployeeStatusEntity ToEntity(this EmployeeStatusDomain domainStatus)
    {
        return domainStatus switch
        {
            EmployeeStatusDomain.Active => EmployeeStatusEntity.Active,
            EmployeeStatusDomain.Inactive => EmployeeStatusEntity.Inactive,
            _ => throw new ArgumentOutOfRangeException(nameof(domainStatus), $"Unknown domain status: {domainStatus}")
        };
    }

    public static EmployeeStatusDomain ToDomain(this EmployeeStatusEntity entityStatus)
    {
        return entityStatus switch
        {
            EmployeeStatusEntity.Active => EmployeeStatusDomain.Active,
            EmployeeStatusEntity.Inactive => EmployeeStatusDomain.Inactive,
            _ => throw new ArgumentOutOfRangeException(nameof(entityStatus), $"Unknown entity status: {entityStatus}")
        };
    }
}