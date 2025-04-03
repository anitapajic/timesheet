using Timesheet.Infrastructure.Entities;

namespace Timesheet.Infrastructure.Mappers.Impl;

public class CountryMapper : ICustomMapper<TimesheetApp.Domain.Models.Country, Country>
{
    public Country ToEntity(TimesheetApp.Domain.Models.Country domainModel)
    {
        return new Country
        {
            Id = domainModel.Id,
            Name = domainModel.Name
        };
    }

    public TimesheetApp.Domain.Models.Country ToDomain(Country entity)
    {
        return new TimesheetApp.Domain.Models.Country
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public void MapToExistingEntity(TimesheetApp.Domain.Models.Country domainModel, Country entity)
    {
        entity.Name = domainModel.Name;
    }
}