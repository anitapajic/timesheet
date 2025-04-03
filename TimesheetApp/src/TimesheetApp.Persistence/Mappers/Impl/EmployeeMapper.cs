using Timesheet.Infrastructure.Entities;
using Timesheet.Infrastructure.Extensions;

namespace Timesheet.Infrastructure.Mappers.Impl;

public class EmployeeMapper : ICustomMapper<TimesheetApp.Domain.Models.Employee, Employee>
{
    public Employee ToEntity(TimesheetApp.Domain.Models.Employee domainModel)
    {
        return new Employee
        {
            Id = domainModel.Id,
            Name = domainModel.Name,
            Username = domainModel.Username,
            Email = domainModel.Email,
            Password = domainModel.Password,
            HoursPerWeek = domainModel.HoursPerWeek,
            Role = domainModel.Role.ToEntity(),
            EmployeeStatus = domainModel.EmployeeStatus.ToEntity(),
        };
    }

    public TimesheetApp.Domain.Models.Employee ToDomain(Employee entity)
    {
        return new TimesheetApp.Domain.Models.Employee 
        {
            Id = entity.Id,
            Name = entity.Name,
            Username = entity.Username,
            Email = entity.Email,
            Password = entity.Password,
            HoursPerWeek = entity.HoursPerWeek,
            Role = entity.Role.ToDomain(),
            EmployeeStatus = entity.EmployeeStatus.ToDomain(),
        };
    }

    public void MapToExistingEntity(TimesheetApp.Domain.Models.Employee domainModel, Employee entity)
    {
        entity.Name = domainModel.Name;
        entity.Username = domainModel.Username;
        entity.Email = domainModel.Email;
        entity.Password = domainModel.Password;
        entity.HoursPerWeek = domainModel.HoursPerWeek;
        entity.Role = domainModel.Role.ToEntity();
        entity.EmployeeStatus = domainModel.EmployeeStatus.ToEntity();
    }
}