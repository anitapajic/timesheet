using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Mappers.Impl;

public class CategoryMapper : ICustomMapper<TimesheetApp.Domain.Models.Category, Category>
{
    public Category ToEntity(TimesheetApp.Domain.Models.Category domainModel)
    {
        return new Category
        {
            Id = domainModel.Id,
            Name = domainModel.Name
        };
    }

    public TimesheetApp.Domain.Models.Category ToDomain(Category entity)
    {
        return new TimesheetApp.Domain.Models.Category
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public void MapToExistingEntity(TimesheetApp.Domain.Models.Category domainModel, Category entity)
    {
        entity.Name = domainModel.Name;
    }
}