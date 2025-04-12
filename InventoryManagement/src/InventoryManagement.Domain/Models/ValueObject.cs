namespace InventoryManagement.Domain.Models;
public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x,y) => x ^ y);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        return ((ValueObject)obj).GetEqualityComponents()
            .SequenceEqual(GetEqualityComponents());
    }
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }

}
