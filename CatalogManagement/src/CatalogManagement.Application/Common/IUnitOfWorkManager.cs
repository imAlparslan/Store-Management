namespace CatalogManagement.Application.Common;
public interface IUnitOfWorkManager
{
    bool IsUnitOfWorkManagerStarted();
    void StartUnitOfWork();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}