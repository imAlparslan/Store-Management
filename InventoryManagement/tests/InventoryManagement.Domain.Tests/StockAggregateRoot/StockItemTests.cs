using InventoryManagement.Domain.StockAggregateRoot.Entities;

namespace InventoryManagement.Domain.Tests.StockAggregateRoot;
public class StockItemTests
{

    [Theory]
    [InlineData(-1)]
    public void Creating_StockItem_ShouldThrowDomainException_WhenQuantityNegative(int quantity)
    {
        var createStockItemAction = () => new StockItem(ItemId.CreateUnique(), quantity: new(quantity), new(1));

        Should.Throw<DomainException>(createStockItemAction)
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(StockErrors.InvalidQuantityError.Description),
                x => x.Code.ShouldBe(StockErrors.InvalidQuantityError.Code));
    }

    [Theory]
    [InlineData(-1)]
    public void Creating_StockItem_ShouldThrowDomainException_WhenCapacityNegative(int capacity)
    {
        var createStockItemAction = () => new StockItem(ItemId.CreateUnique(), quantity: new(1), capacity: new(capacity));

        Should.Throw<DomainException>(createStockItemAction)
            .ShouldSatisfyAllConditions(
                x => x.Message.ShouldBe(StockErrors.InvalidCapacityError.Description),
                x => x.Code.ShouldBe(StockErrors.InvalidCapacityError.Code));
    }
}
