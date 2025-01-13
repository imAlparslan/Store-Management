namespace StoreDefinition.Application.Common.Interfaces;
public interface IUnitOfWorkManager
{
    bool IsUnitOfWorkManagerStarted();
    void StartUnitOfWork();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
