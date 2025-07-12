using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot.Entities;

namespace InventoryManagement.Domain.Tests.Factories;

public static class StockFactory
{
    public static Stock CreateEmpty()
    {
        return new Stock(
            Guid.CreateVersion7(),
            new List<Guid>(),
            new List<StockItem>()
        );
