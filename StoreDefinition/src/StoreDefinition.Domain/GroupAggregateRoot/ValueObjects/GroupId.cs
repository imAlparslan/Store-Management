using StoreDefinition.Domain.Common.Models;

namespace StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
public sealed class GroupId : ValueObject
{
    public Guid Value { get; }
    private GroupId(Guid value)
    {
        Value = value;
    }

    public static GroupId CreateUnique()
    {
        return new GroupId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(GroupId groupId) => groupId.Value;

    public static implicit operator GroupId(Guid groupId) => new GroupId(groupId);
}
