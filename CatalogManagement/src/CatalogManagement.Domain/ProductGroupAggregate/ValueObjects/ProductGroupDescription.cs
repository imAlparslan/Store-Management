using Ardalis.GuardClauses;
using CatalogManagement.Domain.Common.Models;

namespace CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
public sealed class ProductGroupDescription : ValueObject
{
    public string Value { get; private set; }
    public ProductGroupDescription(string description)
    {
        Value = Guard.Against.NullOrWhiteSpace(description);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
