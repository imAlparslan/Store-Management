namespace InventoryManagement.Domain.Tests.StockAggregateRoot;

public class StockItemCapacityTests
{
    [Theory]
    [InlineData(0, 5, 5)]
    [InlineData(10, 5, 15)]
    public void IncreaseStockItemCapacity_ShouldIncreaseCapacity_WhenStockItemExists(int initialCapacity, int increasedCapacity, int expectedCapacity)
    {
        var defaultStock = StockFactory.CreateEmpty();
        var defaultItem = StockItemFactory.CreateDefault(initialCapacity: initialCapacity);
        defaultStock.TryAddItem(defaultItem.ItemId, defaultItem.Quantity.Value, defaultItem.Capacity.Value);

        defaultStock.IncreaseStockItemCapacity(defaultItem.Id, increasedCapacity);

        var item = defaultStock.StockItems.First(x => x.Id == defaultItem.Id);
        item.Capacity.Value.ShouldBe(expectedCapacity);
    }
   
    [Fact]
    public void IncreaseStockItemCapacity_ShouldThrowException_WhenStockItemDoesNotExist()
    {
        var stock = StockFactory.CreateEmpty();

        var action = () => stock.IncreaseStockItemCapacity(StockItemId.CreateUnique(), 5);

        action.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                x => x.Code.ShouldBe(StockErrors.StockItemNotFound.Code),
                x => x.Message.ShouldBe(StockErrors.StockItemNotFound.Description));
    }
}