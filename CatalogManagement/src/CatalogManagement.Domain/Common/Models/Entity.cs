namespace CatalogManagement.Domain.Common.Models;
public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    public TId Id { get; init; } = default!;

    protected Entity(TId id)
    {
        Id = id;
    }
    protected Entity()
    {

    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return ((Entity<TId>)obj).Id.Equals(Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }
}
