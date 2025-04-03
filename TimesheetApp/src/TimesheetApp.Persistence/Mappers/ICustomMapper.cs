namespace Timesheet.Infrastructure.Mappers;

public interface ICustomMapper<TDomain, TEntity>
{
    TDomain ToDomain(TEntity entity);
    TEntity ToEntity(TDomain domainModel);
    void MapToExistingEntity(TDomain domainModel, TEntity entity);
}