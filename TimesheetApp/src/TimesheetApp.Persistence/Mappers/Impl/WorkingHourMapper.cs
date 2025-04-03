using Timesheet.Infrastructure.Extensions;
using TimesheetApp.Domain.Models;

namespace Timesheet.Infrastructure.Mappers.Impl;

public class WorkingHourMapper : ICustomMapper<WorkingHour, Entities.WorkingHour>
{
    public Entities.WorkingHour ToEntity(WorkingHour domainModel)
    {
        return new Entities.WorkingHour
        {
           Id = domainModel.Id,
           ClientId = domainModel.ClientId,
           ProjectId = domainModel.ProjectId,
           CategoryId = domainModel.CategoryId,
           Description = domainModel.Description,
           Time = domainModel.Time,
           Overtime = domainModel.Overtime,
           EmployeeId = domainModel.EmployeeId,
           Date = domainModel.Date,
        };
    }
    
    public WorkingHour ToDomain(Entities.WorkingHour entity)
    {
        return new WorkingHour
        {
            Id = entity.Id,
            ClientId = entity.ClientId,
            Client = entity.Client?.ToOverview(),
            ProjectId = entity.ProjectId,
            Project = entity.Project?.ToOverview(),
            CategoryId = entity.CategoryId,
            Category = entity.Category?.ToOverview(),
            Description = entity.Description,
            Time = entity.Time,
            Overtime = entity.Overtime,
            EmployeeId = entity.EmployeeId,
            Employee = entity.Employee?.ToOverview(),
            Date = entity.Date,
        };
    }
    public void MapToExistingEntity(WorkingHour domainModel, Entities.WorkingHour entity)
    {
        entity.ClientId = domainModel.ClientId;
        entity.ProjectId = domainModel.ProjectId;
        entity.CategoryId = domainModel.CategoryId;
        entity.Description = domainModel.Description;
        entity.Time = domainModel.Time;
        entity.Overtime = domainModel.Overtime;
        entity.EmployeeId = domainModel.EmployeeId;
        entity.Date = domainModel.Date;
    }
}