using Microsoft.EntityFrameworkCore;
using Timesheet.Infrastructure.Context;
using Timesheet.Infrastructure.Mappers;
using Timesheet.Infrastructure.Repository.Base;
using TimesheetApp.Core.Repositories.ProjectRepository;
using TimesheetApp.Domain.Models;

namespace Timesheet.Infrastructure.Repository.ProjectRepository;

public class ProjectRepository : BaseRepository<Project, Entities.Project> , IProjectRepository
{
    public ProjectRepository(TimesheetAppContext context, ICustomMapper<Project, Entities.Project> mapper)
        : base(context, mapper)
    {
        
    }
    
    protected override IQueryable<Entities.Project> GetQueryable()
    {
        return base.GetQueryable()
            .Include(c => c.Client)
            .Include(c => c.Lead)
            .Include(c => c.Employees);
    }
    
    public override async Task<Project> CreateAsync(Project domainModel, CancellationToken cancellationToken)
    {
        var entity = Mapper.ToEntity(domainModel);
        entity.DateCreated = DateTime.UtcNow;
        entity.IsDeleted = false;

        var lead = Context.Employees.FirstOrDefault(c => c.Id == domainModel.LeadId);
        if (lead != null)
        {
            entity.Lead = lead;
        }
        var client = Context.Clients.FirstOrDefault(c => c.Id == domainModel.ClientId);
        if (client != null)
        {
            entity.Client = client;
        }

        Context.Set<Entities.Project>().Add(entity);

        return Mapper.ToDomain(entity);
    }

    public override async Task<Project> UpdateAsync(Project domainModel, CancellationToken cancellationToken)
    {
        var entity = await GetQueryable()
            .Include(p => p.Employees) 
            .FirstOrDefaultAsync(e => e.Id == domainModel.Id, cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException("Project not found.");
        }
        
        Mapper.MapToExistingEntity(domainModel, entity);
        entity.DateUpdated = DateTime.UtcNow;

        if (domainModel.EmployeeIds == null) return Mapper.ToDomain(entity);
        {
            var newEmployees = await Context.Employees
                .Where(e => domainModel.EmployeeIds.Contains(e.Id))
                .ToListAsync(cancellationToken);
            
            entity.Employees.Clear();
            entity.Employees.AddRange(newEmployees);
            Context.Entry(entity).Collection(e => e.Employees).IsModified = true;
        }

        return Mapper.ToDomain(entity);
    }
}