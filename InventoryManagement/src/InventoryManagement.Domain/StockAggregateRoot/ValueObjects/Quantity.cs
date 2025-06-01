using Ardalis.GuardClauses;
using InventoryManagement.Domain.StockAggregateRoot.Errors;
using InventoryManagement.Domain.StockAggregateRoot.Exceptions;

namespace InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

public class Quantity : ValueObject
{
    public int Value { get; private set; }

    public Quantity(int value)
    {
        Value = Guard.Against.Negative(
            value,
            exceptionCreator: () => StockException.Create(StockErrors.InvalidQuantityError));
    }
    private Quantity()
    {
        // For EF Core
    }
    public void SetQuantity(int value)
    {
        Value = Guard.Against.Negative(
            value,
            exceptionCreator: () => StockException.Create(StockErrors.InvalidQuantityError));
    }
    public void Increase(int amount)
    {
        Value += Guard.Against.Negative(amount,
                exceptionCreator: () => StockException.Create(StockErrors.InvalidQuantityError));

    }
    public void Decrease(int amount)
    {
        Guard.Against.Negative(amount, exceptionCreator: () => StockException.Create(StockErrors.InvalidQuantityError));

        Value = Guard.Against.Negative(Value - amount,
            exceptionCreator: () => StockException.Create(StockErrors.InsufficientStockError));
    }

    public override string ToString()
    {
        return Value.ToString();
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
