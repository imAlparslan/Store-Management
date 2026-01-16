using InventoryManagement.Application.Stocks.Commands.AddStockItem;

namespace InventoryManagement.Application.Tests.Factories.StockItemFactories;
internal class AddStockItemCommandFactory
{
    internal static AddStockItemCommand CreateValid(
        Guid? stockId = null,
        Guid? itemId = null,
        int? initialQuantity = null,
        int? initialCapacity = null)
        => new AddStockItemCommand(
            stockId ?? Guid.CreateVersion7(),
            itemId ?? Guid.CreateVersion7(),
            initialQuantity ?? 10,
            initialCapacity ?? 10);
}