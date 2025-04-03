using Microsoft.EntityFrameworkCore;
using Timesheet.Infrastructure.Context;
using Timesheet.Infrastructure.Mappers;
using Timesheet.Infrastructure.Mappers.Impl;
using Timesheet.Infrastructure.Repository.Base;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CountryRepository;
using TimesheetApp.Domain.Models;

namespace Timesheet.Infrastructure.Repository.CountryRepository;

public class CountryRepository : BaseRepository<Country, Entities.Country>, ICountryRepository
{
    private readonly ICustomMapper<Country, Entities.Country> _mapper;

    public CountryRepository(TimesheetAppContext context, ICustomMapper<Country, Entities.Country> mapper)
        : base(context, mapper)
    {
       _mapper = mapper;
    }
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await Context.Countries.AnyAsync(c => c.Name == name && c.IsDeleted == false);
    }
    
    public async Task<Country> GetByNameAsync(string name)
    {
        var countryEntity = await Context.Countries
            .AsNoTracking()  
            .FirstOrDefaultAsync(c => c.Name == name && c.IsDeleted == false);
        return (countryEntity != null ? _mapper.ToDomain(countryEntity) : null)!;
    }
}