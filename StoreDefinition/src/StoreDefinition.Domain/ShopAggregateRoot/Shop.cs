using StoreDefinition.Domain.Common.Models;
using StoreDefinition.Domain.ShopAggregateRoot.Entities;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.ShopAggregateRoot;
public class Shop : AggregateRoot<ShopId>
{
    public ShopDescription Description { get; private set; } = null!;
    public ShopAddress Address { get; private set; } = null!;

    private readonly List<Guid> _groupIds = new();

    public IReadOnlyList<Guid> GroupIds => _groupIds;

    public Shop(
      ShopDescription description,
      ShopAddress address,
      ShopId? Id = null) : base(Id ?? ShopId.CreateUnique())
    {
        Description = description;
        Address = address;
    }
    public Shop()
    {

    }

    public bool AddGroup(Guid groupId)
    {

        if (!HasGroup(groupId))
        {
            _groupIds.Add(groupId);
            return true;
        }
        return false;
    }
    public bool RemoveGroup(Guid groupId) => _groupIds.Remove(groupId);
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
