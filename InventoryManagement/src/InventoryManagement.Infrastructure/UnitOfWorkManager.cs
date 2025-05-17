using InventoryManagement.Application.Common;
using InventoryManagement.Infrastructure.Persistence;

namespace InventoryManagement.Infrastructure;
public class UnitOfWorkManager(InventoryDbContext dbContext) : IUnitOfWorkManager
{
    private bool _isUnitOfWorkStarted = false;

    private readonly InventoryDbContext _dbContext = dbContext;

    public bool IsUnitOfWorkManagerStarted() => _isUnitOfWorkStarted;

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void StartUnitOfWork()
    {
        _isUnitOfWorkStarted = true;
    }
}
