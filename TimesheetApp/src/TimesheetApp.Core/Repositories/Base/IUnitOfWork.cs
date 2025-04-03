namespace TimesheetApp.Core.Repositories.Base;

public interface IUnitOfWork
{
    Task Save(CancellationToken cancellationToken);
}