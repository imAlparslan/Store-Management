using StoreDefinition.Domain.ShopAggregateRoot.Entities;
using StoreDefinition.Domain.ShopAggregateRoot.Events;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.ShopAggregateRoot;
public class Shop : AggregateRoot<ShopId>
{
    public ShopDescription Description { get; private set; } = null!;
    public ShopAddress Address { get; private set; } = null!;

    private readonly List<Guid> _groupIds = new();

    public IReadOnlyList<Guid> GroupIds => _groupIds;

    internal Shop(
      ShopDescription description,
      ShopAddress address,
      IReadOnlyList<Guid> groupIds,
      ShopId? Id = null) : base(Id ?? ShopId.CreateUnique())
    {
        _groupIds = groupIds.ToList();
        Description = description;
        Address = address;
    }
    private Shop()
    {

    }

    public static Shop CreateNew(ShopDescription description,
      ShopAddress address,
      IReadOnlyList<Guid> groupIds)
    {
        var shop = new Shop(description, address, groupIds);
        shop.AddDomainEvent(new ShopCreatedDomainEvent(shop));
        return shop;
    }

    public bool AddGroup(Guid groupId)
    {
        if (!HasGroup(groupId))
        {
            AddDomainEvent(new GroupAddedToShopDomainEvent(Id, groupId));
            _groupIds.Add(groupId);
            return true;
        }
        return false;
    }
    public bool RemoveGroup(Guid groupId)
    {
        var isRemoved = _groupIds.Remove(groupId);
        if (isRemoved)
        {
            AddDomainEvent(new GroupRemovedFromShopDomainEvent(Id, groupId));
        }
        return isRemoved;
    }
    public bool HasGroup(Guid groupId) => _groupIds.Contains(groupId);
    public void ChangeDescription(ShopDescription description)
    {
        Description = description;
    }
    public void ChangeAddress(ShopAddress address)
    {
        Address = address;
    }

}
