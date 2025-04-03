using Microsoft.EntityFrameworkCore;
using Timesheet.Infrastructure.Context;
using Timesheet.Infrastructure.Mappers;
using Timesheet.Infrastructure.Repository.Base;
using TimesheetApp.Core.Repositories.Base;
using TimesheetApp.Core.Repositories.CategoryRepository;
using TimesheetApp.Domain.Models;

namespace Timesheet.Infrastructure.Repository.CategoryRepository;

public class CategoryRepository : BaseRepository<Category, Entities.Category>, ICategoryRepository
{
    public CategoryRepository(TimesheetAppContext context, ICustomMapper<Category, Entities.Category> mapper)
        : base(context, mapper)
    {
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await Context.Categories.AnyAsync(c => c.Name == name && c.IsDeleted == false);
    }
}