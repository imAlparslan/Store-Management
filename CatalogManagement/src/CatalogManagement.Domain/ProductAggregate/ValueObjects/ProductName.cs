using Ardalis.GuardClauses;
using CatalogManagement.Domain.Common.Models;

namespace CatalogManagement.Domain.ProductAggregate.ValueObjects;
public class ProductName : ValueObject
{
    public string Value { get; private set; }

    public ProductName(string name)
    {
        Value = Guard.Against.NullOrWhiteSpace(name);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductName name) => name.Value;
}
