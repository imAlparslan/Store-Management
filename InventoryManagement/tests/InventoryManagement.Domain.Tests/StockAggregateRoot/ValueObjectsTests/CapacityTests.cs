using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Domain.Tests.StockAggregateRoot.ValueObjectsTests;

public class CapacityTests
{

    [Fact]
    public void CreatingCapacity_ShouldSetValue_WhenValidValue()
    {
        var capacity = new Capacity(10);

        capacity.Value.ShouldBe(10);
    }

    [Theory]
    [InlineData(-1)]
    public void CreatingCapacity_ShouldThrowStockException_WhenNegativeValue(int value)
    {
        var createCapacity = () => new Capacity(value);

        createCapacity.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(StockErrors.InvalidCapacityError.Code),
                ex => ex.Message.ShouldBe(StockErrors.InvalidCapacityError.Description)
            );
    }
    [Fact]
    public void SetCapacity_ShouldUpdateValue_WhenValidValue()
    {
        var capacity = new Capacity(5);
        var newValue = 15;

        capacity.SetCapacity(newValue);

        capacity.Value.ShouldBe(newValue);
    }
    [Theory]
    [InlineData(-5)]
    public void SetCapacity_ShouldThrowStockException_WhenNegativeValue(int value)
    {
        var capacity = new Capacity(5);
        var setCapacity = () => capacity.SetCapacity(value);

        setCapacity.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(StockErrors.InvalidCapacityError.Code),
                ex => ex.Message.ShouldBe(StockErrors.InvalidCapacityError.Description)
            );
    }
    [Fact]
    public void IncreaseCapacity_ShouldIncreaseValue_WhenValidAmount()
    {
        var capacity = new Capacity(5);
        var increaseAmount = 10;

        capacity.IncreaseCapacity(increaseAmount);

        capacity.Value.ShouldBe(15);
    }
    [Theory]
    [InlineData(-5)]
    public void IncreaseCapacity_ShouldThrowStockException_WhenNegativeAmount(int amount)
    {
        var capacity = new Capacity(5);
        var increaseCapacity = () => capacity.IncreaseCapacity(amount);

        increaseCapacity.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(StockErrors.InvalidCapacityError.Code),
                ex => ex.Message.ShouldBe(StockErrors.InvalidCapacityError.Description)
            );
    }
    [Theory]
    [InlineData(10, 10, 0)]
    [InlineData(10, 5, 5)]
    public void DecreaseCapacity_ShouldDecreaseValue_WhenValidAmount(int initial, int decreaseAmount, int expected)
    {
        var capacity = new Capacity(initial);
        capacity.DecreaseCapacity(decreaseAmount);

        capacity.Value.ShouldBe(expected);
    }
    [Fact]
    public void DecreaseCapacity_ShouldThrowStockException_WhenValueNegative()
    {
        var capacity = new Capacity(10);
        var decreaseAmount = 15;

        var decreaseCapacity = () => capacity.DecreaseCapacity(decreaseAmount);

        decreaseCapacity.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(StockErrors.InsufficientCapacityError.Code),
                ex => ex.Message.ShouldBe(StockErrors.InsufficientCapacityError.Description)
            );
    }
    [Theory]
    [InlineData(-5)]
    public void DecreaseCapacity_ShouldThrowStockException_WhenNegativeAmount(int amount)
    {
        var capacity = new Capacity(10);
        var decreaseCapacity = () => capacity.DecreaseCapacity(amount);

        decreaseCapacity.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(StockErrors.InvalidCapacityError.Code),
                ex => ex.Message.ShouldBe(StockErrors.InvalidCapacityError.Description)
            );
    }
}