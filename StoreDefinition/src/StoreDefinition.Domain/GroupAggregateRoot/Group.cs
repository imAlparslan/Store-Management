using StoreDefinition.Domain.Common.Models;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.GroupAggregateRoot;
public sealed class Group : AggregateRoot<GroupId>
{
    public GroupName Name { get; private set; } = null!;
    public GroupDescription Description { get; private set; } = null!;
    private readonly HashSet<Guid> _shopIds = new();
    public IReadOnlyCollection<Guid> ShopIds => _shopIds;

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
    public bool AddShop(Guid shopId) => _shopIds.Add(shopId);
    public bool RemoveShop(Guid shopId) => _shopIds.Remove(shopId);
    public bool HasShop(Guid shopId) => _shopIds.Contains(shopId);

    public Group()
    {

    }
}
