namespace StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
public sealed class GroupId : BaseId
{
    private GroupId(Guid value) : base(value)
    {
    }
    private GroupId() : base()
    {
    }
    public static GroupId CreateUnique()
    {
        return new GroupId(Guid.NewGuid());
    }

    public static implicit operator Guid(GroupId groupId) => groupId.Value;

    public static implicit operator GroupId(Guid groupId) => new GroupId(groupId);
}
