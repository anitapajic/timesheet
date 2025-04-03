using Microsoft.EntityFrameworkCore;
using Timesheet.Infrastructure.Context;
using Timesheet.Infrastructure.Mappers;
using Timesheet.Infrastructure.Repository.Base;
using TimesheetApp.Core.Repositories.ClientRepository;
using TimesheetApp.Domain.Models;

namespace Timesheet.Infrastructure.Repository.ClientRepository;

public class ClientRepository : BaseRepository<Client, Entities.Client>, IClientRepository
{
    public ClientRepository(TimesheetAppContext context, ICustomMapper<Client, Entities.Client> mapper)
        : base(context, mapper)
    {
        
    }
    
    protected override IQueryable<Entities.Client> GetQueryable()
    {
        return base.GetQueryable().Include(c => c.Country);
    }
    
    public override async Task<Client> CreateAsync(Client domainModel, CancellationToken cancellationToken)
    {
        var entity = Mapper.ToEntity(domainModel);
        entity.DateCreated = DateTime.UtcNow;
        entity.IsDeleted = false;

        var country = Context.Countries.FirstOrDefault(c => c.Id == domainModel.Country.Id);
        if (country != null)
        {
            entity.Country = country;
        }

        Context.Set<Entities.Client>().Add(entity);

        return Mapper.ToDomain(entity);
    }
}