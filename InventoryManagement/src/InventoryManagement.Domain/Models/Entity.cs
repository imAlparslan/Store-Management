namespace InventoryManagement.Domain.Models;
public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; init; } = default!;

    protected Entity(Guid id)
    {
        Id = id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }
        return Equals(((Entity)obj).Id.Equals(Id));
    }
    public bool Equals(Entity? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !Equals(left, right);
    }
}
