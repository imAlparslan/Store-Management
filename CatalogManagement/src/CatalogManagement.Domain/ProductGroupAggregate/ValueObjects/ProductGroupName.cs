using Ardalis.GuardClauses;
using CatalogManagement.Domain.Common.Models;

namespace CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
public class ProductGroupName : ValueObject
{
    public string Value { get; private set; }
    public ProductGroupName(string name)
    {
        Value = Guard.Against.NullOrWhiteSpace(name);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
