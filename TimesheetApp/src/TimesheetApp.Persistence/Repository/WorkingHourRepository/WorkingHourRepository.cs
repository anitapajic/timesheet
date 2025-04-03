using Microsoft.EntityFrameworkCore;
using Timesheet.Infrastructure.Context;
using Timesheet.Infrastructure.Mappers;
using Timesheet.Infrastructure.Repository.Base;
using TimesheetApp.Core.Repositories.WorkingHourRepository;
using TimesheetApp.Domain.Models;

namespace Timesheet.Infrastructure.Repository.WorkingHourRepository;

public class WorkingHourRepository : BaseRepository<WorkingHour, Entities.WorkingHour>, IWorkingHourRepository
{
    public WorkingHourRepository(TimesheetAppContext context, ICustomMapper<WorkingHour, Entities.WorkingHour> mapper)
        : base(context, mapper)
    {
        
    }
    
    protected override IQueryable<Entities.WorkingHour> GetQueryable()
    {
        return base.GetQueryable()
            .Include(c => c.Client)
            .Include(c => c.Employee)
            .Include(c => c.Project)
            .Include(c => c.Category);
    }
    
    public override async Task<WorkingHour> CreateAsync(WorkingHour domainModel, CancellationToken cancellationToken)
    {
        var entity = Mapper.ToEntity(domainModel);
        entity.DateCreated = DateTime.UtcNow;
        entity.IsDeleted = false;

        var employee = Context.Employees.FirstOrDefault(c => c.Id == domainModel.EmployeeId);
        if (employee != null)
        {
            entity.Employee = employee;
        }
        var client = Context.Clients.FirstOrDefault(c => c.Id == domainModel.ClientId);
        if (client != null)
        {
            entity.Client = client;
        }
        var project = Context.Projects.FirstOrDefault(c => c.Id == domainModel.ProjectId);
        if (project != null)
        {
            entity.Project = project;
        }
        var category = Context.Categories.FirstOrDefault(c => c.Id == domainModel.CategoryId);
        if (category != null)
        {
            entity.Category = category;
        }

        Context.Set<Entities.WorkingHour>().Add(entity);

        return Mapper.ToDomain(entity);
    }
}