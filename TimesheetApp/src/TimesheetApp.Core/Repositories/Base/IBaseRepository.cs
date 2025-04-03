namespace TimesheetApp.Core.Repositories.Base;

public interface IBaseRepository<TDomain> where TDomain : class
{
    Task<TDomain> Get(Guid id, CancellationToken cancellationToken);
    Task<List<TDomain>> GetAll(CancellationToken cancellationToken);
    Task<TDomain> CreateAsync(TDomain entity, CancellationToken cancellationToken);
    Task<TDomain> UpdateAsync(TDomain entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}