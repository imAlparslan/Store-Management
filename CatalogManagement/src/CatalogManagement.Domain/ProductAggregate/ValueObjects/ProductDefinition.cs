using Ardalis.GuardClauses;
using CatalogManagement.Domain.Common.Models;

namespace CatalogManagement.Domain.ProductAggregate.ValueObjects;
public sealed class ProductDefinition : ValueObject
{
    public string Value { get; private set; }
    public ProductDefinition(string definition)
    {
        Value = Guard.Against.NullOrWhiteSpace(definition);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductDefinition definition) => definition.Value;
}
