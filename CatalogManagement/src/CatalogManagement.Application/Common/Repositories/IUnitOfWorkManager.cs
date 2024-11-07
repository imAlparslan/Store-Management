namespace CatalogManagement.Application.Common.Repositories;
public interface IUnitOfWorkManager
{
    bool IsUnitOfWorkManagerStarted();
    void StartUnitOfWork();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}