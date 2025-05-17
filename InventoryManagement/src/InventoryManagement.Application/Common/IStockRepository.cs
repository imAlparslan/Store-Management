using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Application.Common;
public interface IStockRepository
{
    Task<Stock> InsertShopAsync(Stock stock, CancellationToken cancellation = default);
    Task<Stock?> GetStockByStockId(StockId stockId, CancellationToken cancellation = default);
    Task<Stock?> GetStockByStoreId(StoreId storeId, CancellationToken cancellation = default);
    Task<IEnumerable<Stock>> GetAllStocksAsync(CancellationToken cancellation = default);
    Task<Stock> UpdateStock(Stock stock, CancellationToken cancellation = default);
    Task<bool> DeleteStockAsync(Stock stock, CancellationToken cancellation = default);
} 

