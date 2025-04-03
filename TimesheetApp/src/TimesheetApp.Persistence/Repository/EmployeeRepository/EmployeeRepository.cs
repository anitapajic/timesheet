using Microsoft.EntityFrameworkCore;
using Timesheet.Infrastructure.Context;
using Timesheet.Infrastructure.Mappers;
using Timesheet.Infrastructure.Repository.Base;
using TimesheetApp.Core.Repositories.EmployeeRepository;
using TimesheetApp.Domain.Models;

namespace Timesheet.Infrastructure.Repository.EmployeeRepository;

public class EmployeeRepository : BaseRepository<Employee, Entities.Employee>, IEmployeeRepository
{
    public EmployeeRepository(TimesheetAppContext context, ICustomMapper<Employee, Entities.Employee> mapper)
        : base(context, mapper)
    {
        
    }
    
    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await Context.Employees.AnyAsync(c => c.Username == username && c.IsDeleted == false);
    }
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await Context.Employees.AnyAsync(c => c.Email == email && c.IsDeleted == false);
    }

    public async Task<List<Employee>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        var employees = await GetQueryable().Where(c => ids.Contains(c.Id) && c.IsDeleted==false)
            .ToListAsync(cancellationToken);
        return employees.Select(e => Mapper.ToDomain(e)).ToList();
    }
}