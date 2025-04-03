using Microsoft.EntityFrameworkCore;
using Timesheet.Infrastructure.Context;
using Timesheet.Infrastructure.Entities.Base;
using Timesheet.Infrastructure.Mappers;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Domain.Base;

namespace Timesheet.Infrastructure.Repository.Base;

public class BaseRepository<TDomain, TEntity> : IBaseRepository<TDomain>
    where TEntity : BaseEntity
    where TDomain : BaseModel
{
    protected readonly TimesheetAppContext Context;
    protected readonly ICustomMapper<TDomain, TEntity> Mapper;

    public BaseRepository(TimesheetAppContext context, ICustomMapper<TDomain, TEntity> mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    protected virtual IQueryable<TEntity> GetQueryable()
    {
        return Context.Set<TEntity>();
    }

    public async Task<TDomain> Get(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetQueryable()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        return (entity != null ? Mapper.ToDomain(entity) : null)!;
    }

    public async Task<List<TDomain>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await GetQueryable().Where(e => !e.IsDeleted).ToListAsync(cancellationToken);
        return entities.Select(e => Mapper.ToDomain(e)).ToList();
    }

    public virtual async Task<TDomain> CreateAsync(TDomain domainModel, CancellationToken cancellationToken)
    {
        var entity = Mapper.ToEntity(domainModel);
        entity.DateCreated = DateTime.UtcNow;
        entity.IsDeleted = false;
        
        Context.Set<TEntity>().Add(entity);

        return Mapper.ToDomain(entity);
    }


    public virtual async Task<TDomain> UpdateAsync(TDomain domainModel, CancellationToken cancellationToken)
    {
        var entity = await GetQueryable().FirstOrDefaultAsync(e => e.Id == domainModel.Id, cancellationToken);
        if (entity == null)
        {
            throw new KeyNotFoundException("Entity not found");
        }
        
        Mapper.MapToExistingEntity(domainModel, entity);
        entity.DateUpdated = DateTime.UtcNow;
        
        entity = await GetQueryable()
            .FirstOrDefaultAsync(e => e.Id == entity.Id, cancellationToken);
        
        return Mapper.ToDomain(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await Context.Set<TEntity>().FindAsync([id], cancellationToken);
        if (entity != null)
        {
            entity.DateDeleted = DateTime.UtcNow;
            entity.IsDeleted = true;
        }
    }

}
