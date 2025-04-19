using CatalogManagement.Domain.ProductAggregate.Events;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
namespace CatalogManagement.Domain.ProductAggregate;
public sealed class Product : AggregateRoot<ProductId>
{
    public ProductName Name { get; private set; } = null!;
    public ProductCode Code { get; private set; } = null!;
    public ProductDefinition Definition { get; private set; } = null!;

    private readonly List<Guid> _groupIds = new();
    public IReadOnlyList<Guid> GroupIds => _groupIds;

    internal Product(ProductName name,
                   ProductCode code,
                   ProductDefinition definition,
                   IReadOnlyList<Guid> groupIds,
                   ProductId? id = null)
                   : base(id ?? ProductId.CreateUnique())
    {
        Name = name;
        Code = code;
        Definition = definition;
        _groupIds = groupIds.ToList();
    }

    public static Product Create(ProductName name,
                   ProductCode code,
                   ProductDefinition definition,
                   IReadOnlyList<Guid> groupIds)
    {

        var product = new Product(name, code, definition, groupIds);
        product.AddDomainEvent(new ProductCreatedDomainEvent(product));
        return product;
    }

    public bool AddGroup(Guid groupId)
    {
        if (!HasGroup(groupId))
        {
            AddDomainEvent(new NewGroupAddedToProductDomainEvent(groupId, Id));
            _groupIds.Add(groupId);
            return true;
        }
        return false;
    }
    public bool RemoveGroup(Guid groupId)
    {
        if (HasGroup(groupId))
        {
            AddDomainEvent(new GroupRemovedFromProductDomainEvent(groupId, Id));
            return _groupIds.Remove(groupId);
        }
        return false;
    }
    public bool HasGroup(Guid groupId)
    {
        return _groupIds.Contains(groupId);
    }

    public void ChangeName(ProductName name)
    {
        Name = name;
    }
    public void ChangeCode(ProductCode code)
    {
        Code = code;
    }
    public void ChangeDefinition(ProductDefinition definition)
    {
        Definition = definition;
    }

    private Product()
    {

    }
}

