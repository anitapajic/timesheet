using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Core.Repositories.CountryRepository;

public interface ICountryRepository : IBaseRepository<Country>
{
    public Task<bool> ExistsByNameAsync(string name);
    public Task<Country> GetByNameAsync(string name);
}