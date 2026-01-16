using Ardalis.GuardClauses;
using InventoryManagement.Domain.StockAggregateRoot.Errors;
using InventoryManagement.Domain.StockAggregateRoot.Exceptions;

namespace InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

public class Capacity : ValueObject
{
    public int Value { get; private set; }

    public Capacity(int value)
    {
        Value = Guard.Against.Negative(
            value,
            exceptionCreator: () => StockException.Create(StockErrors.InvalidCapacityError));

    }
    private Capacity()
    {
        // For EF Core
    }
    public void SetCapacity(int value)
    {
        Value = Guard.Against.Negative(
            value,
            exceptionCreator: () => StockException.Create(StockErrors.InvalidCapacityError));
    }
    public void IncreaseCapacity(int amount)
    {
        Value += Guard.Against.Negative(
            amount,
            exceptionCreator: () => StockException.Create(StockErrors.InvalidCapacityError));
    }
    public void DecreaseCapacity(int amount)
    {
        Guard.Against.Negative(amount,
            exceptionCreator: () => StockException.Create(StockErrors.InvalidCapacityError));

        Value = Guard.Against.Negative(
             Value - amount,
             exceptionCreator: () => StockException.Create(StockErrors.InsufficientCapacityError));
    }
    public override string ToString()
    {
        return Value.ToString();
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static bool operator ==(Capacity left, int right)
       => left.Value == right;

    public static bool operator !=(Capacity left, int right)
        => left.Value != right;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        throw new NotImplementedException();
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}