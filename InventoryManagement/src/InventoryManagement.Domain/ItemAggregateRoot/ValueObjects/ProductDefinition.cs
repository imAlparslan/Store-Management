using Ardalis.GuardClauses;
using InventoryManagement.Domain.ItemAggregateRoot.Exceptions;

namespace InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;

public class ProductDefinition : ValueObject
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Definition { get; private set; }

    public ProductDefinition(string name, string code, string definition)
    {

        Name = Guard.Against.NullOrWhiteSpace(
            name,
            exceptionCreator: () => ItemException.Create(ItemErrors.InvalidProductName));

        Code = Guard.Against.NullOrWhiteSpace(
            code,
            exceptionCreator: () => ItemException.Create(ItemErrors.InvalidProductCode));

        Definition = Guard.Against.NullOrWhiteSpace(
            definition,
            exceptionCreator: () => ItemException.Create(ItemErrors.InvalidProductDefinition));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    public override string ToString()
        => $"Code: {Code} - Name: {Name}";
}
