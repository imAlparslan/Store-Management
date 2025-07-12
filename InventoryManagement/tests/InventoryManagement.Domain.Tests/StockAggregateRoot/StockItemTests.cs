using InventoryManagement.Domain.StockAggregateRoot.Entities;

namespace InventoryManagement.Domain.Tests.StockAggregateRoot;

public class StockItemTests
{
    [Fact]
    public void Creating_StockItem_ShouldCreateStockItem_WhenValidParameters()
    {
        var stockItem = new StockItem(ItemId.CreateUnique(), quantity: new(1), capacity: new(10));

        stockItem.ShouldNotBeNull();
        stockItem.ItemId.ShouldNotBeNull();
        stockItem.Quantity.Value.ShouldBe(1);
        stockItem.Capacity.Value.ShouldBe(10);
    }

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
    [Theory]
    [InlineData(0, 5, 5)]
    [InlineData(10, 5, 15)]
    public void IncreaseQuantity_ShouldIncreaseStockItemQuantity_WhenValidAmount(int initialQuantity, int increaseAmount, int expectedQuantity)
    {
        StockItem stockItem = StockItemFactory.CreateDefault(initialQuantity: initialQuantity);

        stockItem.IncreaseQuantity(increaseAmount);

        stockItem.Quantity.Value.ShouldBe(expectedQuantity);
    }
    
    
}
