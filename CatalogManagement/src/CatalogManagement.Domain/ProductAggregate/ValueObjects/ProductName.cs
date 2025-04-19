using Ardalis.GuardClauses;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Exceptions;

namespace CatalogManagement.Domain.ProductAggregate.ValueObjects;
public class ProductName : ValueObject
{
    public string Value { get; private set; } = null!;

    public ProductName(string name)
    {
        Value = Guard.Against.NullOrWhiteSpace(
            name,
            exceptionCreator: () => ProductException.Create(ProductError.InvalidName));
    }
    private ProductName()
    {

    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductName name) => name.Value;
}
