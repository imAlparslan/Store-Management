using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Infrastructure.Persistence;

namespace StoreDefinition.Infrastructure;
public class UnitOfWorkManager(StoreDefinitionDbContext storeDefinitionContext) : IUnitOfWorkManager
{
    private bool _isUnitOfWorkStarted = false;

    private readonly StoreDefinitionDbContext catalogDbContext = storeDefinitionContext;

    public bool IsUnitOfWorkManagerStarted() => _isUnitOfWorkStarted;

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await catalogDbContext.SaveChangesAsync(cancellationToken);
    }

    public void StartUnitOfWork()
    {
        _isUnitOfWorkStarted = true;
    }
}
