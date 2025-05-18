using InventoryManagement.Domain.StockAggregateRoot.Entities;
using InventoryManagement.Domain.StockAggregateRoot.Errors;

namespace InventoryManagement.Domain.Tests.StockAggregateRoot;
public class StockItemTests
{

    [Theory]
    [InlineData(-1)]
    public void Creating_StockItem_ShouldThrowDomainException_WhenQuantityNegative(int quantity)
    {
        var createStockItemAction = () => new StockItem(ItemId.CreateUnique(), quantity: quantity);

        Should.Throw<DomainException>(createStockItemAction)
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(StockErrors.InvalidQuantityError.Description),
                x => x.Code.ShouldBe(StockErrors.InvalidQuantityError.Code));
    }

    [Theory]
    [InlineData(-1)]
    public void Creating_StockItem_ShouldThrowDomainException_WhenCapacityNegative(int capacity)
    {
        var createStockItemAction = () => new StockItem(ItemId.CreateUnique(), capacity: capacity);

        Should.Throw<DomainException>(createStockItemAction)
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(StockErrors.InvalidCapacityError.Description),
                x => x.Code.ShouldBe(StockErrors.InvalidCapacityError.Code));
    }
}
