using CatalogManagement.Application.Common;

namespace CatalogManagement.Infrastructure.Persistence;
internal class UnitOfWorkManager : IUnitOfWorkManager
{
    private bool _isUnitOfWorkStarted = false;

    private readonly CatalogDbContext _catalogDbContext;

    public UnitOfWorkManager(CatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public bool IsUnitOfWorkManagerStarted() => _isUnitOfWorkStarted;

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _catalogDbContext.SaveChangesAsync(cancellationToken);
    }

    public void StartUnitOfWork()
    {
        _isUnitOfWorkStarted = true;
    }
}
