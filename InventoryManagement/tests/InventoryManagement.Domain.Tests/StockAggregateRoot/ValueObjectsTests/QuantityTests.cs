using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Domain.Tests.StockAggregateRoot.ValueObjectsTests;

public class QuantityTests
{

    [Fact]
    public void CreatingQuantity_ShouldSetValue_WhenValidValue()
    {
        var value = 10;
        var quantity = new Quantity(value);

        quantity.Value.ShouldBe(value);
    }

    [Theory]
    [InlineData(-1)]
    public void CreatingQuantity_ShouldThrowStockException_WhenNegativeValue(int value)
    {
        var createQuantity = () => new Quantity(value);

        createQuantity.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(StockErrors.InvalidQuantityError.Code),
                ex => ex.Message.ShouldBe(StockErrors.InvalidQuantityError.Description)
            );
    }

    [Fact]
    public void SetQuantity_ShouldUpdateValue_WhenValidValue()
    {
        var quantity = new Quantity(5);
        var newValue = 15;

        quantity.SetQuantity(newValue);

        quantity.Value.ShouldBe(newValue);
    }

    [Theory]
    [InlineData(-5)]
    public void SetQuantity_ShouldThrowStockException_WhenNegativeValue(int value)
    {
        var quantity = new Quantity(5);
        var setQuantity = () => quantity.SetQuantity(value);

        setQuantity.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(StockErrors.InvalidQuantityError.Code),
                ex => ex.Message.ShouldBe(StockErrors.InvalidQuantityError.Description)
            );
    }

    [Fact]
    public void Increase_ShouldIncreaseValue_WhenValidAmount()
    {
        var quantity = new Quantity(5);
        var increaseAmount = 10;

        quantity.Increase(increaseAmount);

        quantity.Value.ShouldBe(15);
    }
    [Theory]
    [InlineData(-5)]
    public void Increase_ShouldThrowStockException_WhenNegativeAmount(int amount)
    {
        var quantity = new Quantity(5);
        var increase = () => quantity.Increase(amount);

        increase.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(StockErrors.InvalidQuantityError.Code),
                ex => ex.Message.ShouldBe(StockErrors.InvalidQuantityError.Description)
            );
    }
    [Theory]
    [InlineData(10, 10, 0)]
    [InlineData(10, 5, 5)]
    public void Decrease_ShouldDecreaseValue_WhenValidAmount(int initial, int decreaseAmount, int expected)
    {
        var quantity = new Quantity(initial);

        quantity.Decrease(decreaseAmount);

        quantity.Value.ShouldBe(expected);
    }

    [Fact]
    public void Decrease_ShouldThrowStockException_WhenValueNegative()
    {
        var quantity = new Quantity(5);
        var decreaseAmount = -1;

        var decrease = () => quantity.Decrease(decreaseAmount);

        decrease.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
            ex => ex.Code.ShouldBe(StockErrors.InvalidQuantityError.Code),
            ex => ex.Message.ShouldBe(StockErrors.InvalidQuantityError.Description));
    }

    [Theory]
    [InlineData(15)]
    public void Decrease_ShouldThrowStockException_WhenInsufficientStockOrNegativeAmount(int decreaseAmount)
    {
        var quantity = new Quantity(10);
        var decrease = () => quantity.Decrease(decreaseAmount);

        decrease.ShouldThrow<StockException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(StockErrors.InsufficientStockError.Code),
                ex => ex.Message.ShouldBe(StockErrors.InsufficientStockError.Description)
            );
    }
}
