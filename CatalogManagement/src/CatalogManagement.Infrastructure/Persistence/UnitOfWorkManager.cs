using CatalogManagement.Application.Common.Repositories;

namespace CatalogManagement.Infrastructure.Persistence;
internal class UnitOfWorkManager(CatalogDbContext catalogDbContext) : IUnitOfWorkManager
{
    private bool _isUnitOfWorkStarted = false;

    private readonly CatalogDbContext catalogDbContext = catalogDbContext;

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
