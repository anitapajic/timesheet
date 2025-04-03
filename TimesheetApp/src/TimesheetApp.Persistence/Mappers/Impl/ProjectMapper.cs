using Timesheet.Infrastructure.Extensions;
using TimesheetApp.Domain.Models;

namespace Timesheet.Infrastructure.Mappers.Impl;

public class ProjectMapper : ICustomMapper<Project, Entities.Project>
{
    public Entities.Project ToEntity(Project domainModel)
    {
        return new Entities.Project
        {
            Id = domainModel.Id,
            Name = domainModel.Name,
            Description = domainModel.Description,
            ProjectStatus = domainModel.ProjectStatus.ToEntity(),
            ClientId = domainModel.ClientId,
            LeadId = domainModel.LeadId,
        };
    }
    
    public Project ToDomain(Entities.Project entity)
    {
        return new Project
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            ClientId = entity.ClientId,
            Client = entity.Client != null ? entity.Client.ToOverview() : null,
            LeadId = entity.LeadId,
            Lead = entity.Lead != null ? entity.Lead.ToOverview() : null,
            ProjectStatus = entity.ProjectStatus.ToDomain(),
            Employees = entity.Employees?.Select(e => e.ToOverview()).ToList() ?? []
        };
    }
    public void MapToExistingEntity(Project domainModel, Entities.Project entity)
    {
        entity.Name = domainModel.Name;
        entity.Description = domainModel.Description;
        entity.ProjectStatus = domainModel.ProjectStatus.ToEntity();
        entity.LeadId = domainModel.LeadId;
        entity.ClientId = domainModel.ClientId;
    }
    
}