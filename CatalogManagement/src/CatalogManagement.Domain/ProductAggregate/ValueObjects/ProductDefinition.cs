using Ardalis.GuardClauses;
using CatalogManagement.Domain.Common.Models;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Exceptions;

namespace CatalogManagement.Domain.ProductAggregate.ValueObjects;
public sealed class ProductDefinition : ValueObject
{
    public string Value { get; private set; } = null!;
    public ProductDefinition(string definition)
    {
        Value = Guard.Against.NullOrWhiteSpace(
            definition,
            exceptionCreator: () => ProductException.Create(ProductError.InvalidDefinition));
    }
    private ProductDefinition()
    {

    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductDefinition definition) => definition.Value;
}
