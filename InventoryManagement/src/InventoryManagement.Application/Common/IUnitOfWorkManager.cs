namespace InventoryManagement.Application.Common;
public interface IUnitOfWorkManager
{
    bool IsUnitOfWorkManagerStarted();
    void StartUnitOfWork();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
