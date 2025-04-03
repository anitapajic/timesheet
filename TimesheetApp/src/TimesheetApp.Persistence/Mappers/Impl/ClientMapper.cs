using Timesheet.Infrastructure.Entities;
using Timesheet.Infrastructure.Extensions;

namespace Timesheet.Infrastructure.Mappers.Impl;

public class ClientMapper : ICustomMapper<TimesheetApp.Domain.Models.Client, Client>
{
    public Client ToEntity(TimesheetApp.Domain.Models.Client domainModel)
    {
        return new Client
        {
            Id = domainModel.Id,
            Name = domainModel.Name,
            Address = domainModel.Address,
            City = domainModel.City,
            PostalCode = domainModel.PostalCode,
        };
    }

    public TimesheetApp.Domain.Models.Client ToDomain(Client entity)
    {
        return new TimesheetApp.Domain.Models.Client
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            City = entity.City,
            PostalCode = entity.PostalCode,
            Country = entity.Country != null ? entity.Country.ToOverview() : null
        };
    }

    public void MapToExistingEntity(TimesheetApp.Domain.Models.Client domainModel, Client entity)
    {
        entity.Name = domainModel.Name;
        entity.Address = domainModel.Address;
        entity.City = domainModel.City;
        entity.PostalCode = domainModel.PostalCode;
        entity.CountryId = domainModel.Country.Id;
        entity.Country = entity.Country;
    }
}