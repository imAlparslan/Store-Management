using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.GroupAggregateRoot;
public class Group : AggregateRoot<GroupId>
{
    public GroupName Name { get; private set; } = null!;
    public GroupDescription Description { get; private set; } = null!;
    private readonly List<Guid> _shopIds = new();
    public IReadOnlyList<Guid> ShopIds => _shopIds;

    public Group(
        GroupName name,
        GroupDescription description,
        GroupId? Id = null) : base(Id ?? GroupId.CreateUnique())
    {
        Name = name;
        Description = description;
    }
    public void ChangeName(GroupName name)
    {
        Name = name;
    }
    public void ChangeDescription(GroupDescription description)
    {
        Description = description;
    }
    public bool AddShop(Guid shopId)
    {
        if (!HasShop(shopId))
        {
            _shopIds.Add(shopId);
            return true;
        }
        return false;
    }
    public bool RemoveShop(Guid shopId) => _shopIds.Remove(shopId);
    public bool HasShop(Guid shopId) => _shopIds.Contains(shopId);

    private Group()
    {

    }
}
