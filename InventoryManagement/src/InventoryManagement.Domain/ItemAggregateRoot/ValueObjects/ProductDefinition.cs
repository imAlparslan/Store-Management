using Ardalis.GuardClauses;

namespace InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;

public class ProductDefinition : ValueObject
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Definition { get; private set; }

    public ProductDefinition(string name, string code, string definition)
    {

        Name = Guard.Against.NullOrWhiteSpace(name);
        Code = Guard.Against.NullOrWhiteSpace(code);
        Definition = Guard.Against.NullOrWhiteSpace(definition);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    public override string ToString()
        => $"Code: {Code} - Name: {Name}";
}
