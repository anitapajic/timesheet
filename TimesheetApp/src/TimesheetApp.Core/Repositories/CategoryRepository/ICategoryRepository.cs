using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.Core.Repositories.CategoryRepository;

public interface ICategoryRepository : IBaseRepository<Category>
{
    public Task<bool> ExistsByNameAsync(string name);
}