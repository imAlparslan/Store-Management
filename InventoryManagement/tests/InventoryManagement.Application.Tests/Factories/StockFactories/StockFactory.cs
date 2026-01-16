using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.Entities;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Application.Tests.Factories.StockFactories;
internal class StockFactory
{
    public static Stock CreateValid(StoreId? storeId = null,
                                    List<Guid>? groups = null,
                                    List<StockItem>? items = null,
                                    StockId? id = null)
        => new Stock(storeId ?? Guid.CreateVersion7(), groups ?? new(), items ?? new(), id ?? StockId.CreateUnique());
}