using Ardalis.GuardClauses;
using CatalogManagement.Domain.Common.Models;

namespace CatalogManagement.Domain.ProductAggregate.ValueObjects;
public class ProductCode : ValueObject
{
    public string Value { get; private set; }

    public ProductCode(string code)
    {
        Value = Guard.Against.NullOrWhiteSpace(code);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductCode code) => code.Value;
}
