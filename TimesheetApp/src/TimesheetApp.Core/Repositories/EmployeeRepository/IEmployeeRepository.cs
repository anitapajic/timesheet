using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Core.Repositories.EmployeeRepository;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    public Task<bool> ExistsByUsernameAsync(string username);
    public Task<bool> ExistsByEmailAsync(string email);
    public Task<List<Employee>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
}