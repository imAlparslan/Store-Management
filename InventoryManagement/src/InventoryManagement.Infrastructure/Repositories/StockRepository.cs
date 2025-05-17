using InventoryManagement.Application.Common;
using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories;
public class StockRepository(InventoryDbContext dbContext, IUnitOfWorkManager unitOfWorkManager)
    : IStockRepository
{
    private readonly InventoryDbContext _dbContext = dbContext;
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;

    public async Task<bool> DeleteStockAsync(Stock stock, CancellationToken cancellation = default)
    {
        var store = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == stock.Id, cancellation);
        if (store is null)
        {
            return false;
        }

        _dbContext.Stocks.Remove(stock);

        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _dbContext.SaveChangesAsync(cancellation);
        }
        return true;

    }

    public async Task<IEnumerable<Stock>> GetAllStocksAsync(CancellationToken cancellation = default)
    {
        return await _dbContext.Stocks.AsNoTracking().ToListAsync(cancellation);
    }

    public async Task<Stock?> GetStockByStockId(StockId stockId, CancellationToken cancellation = default)
    {
        return await _dbContext.Stocks.FindAsync([stockId], cancellation);
    }

    public async Task<Stock?> GetStockByStoreId(StoreId storeId, CancellationToken cancellation = default)
    {
        return await _dbContext.Stocks.AsNoTracking()
            .FirstOrDefaultAsync(x => x.StoreId == storeId, cancellation);
    }

    public async Task<Stock> InsertShopAsync(Stock stock, CancellationToken cancellation = default)
    {
        await _dbContext.Stocks.AddAsync(stock, cancellation);
        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _dbContext.SaveChangesAsync(cancellation);
        }
        return stock;
    }

    public async Task<Stock> UpdateStock(Stock stock, CancellationToken cancellation = default)
    {
        _dbContext.Stocks.Update(stock);
        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _dbContext.SaveChangesAsync(cancellation);
        }
        return stock;
    }
}
