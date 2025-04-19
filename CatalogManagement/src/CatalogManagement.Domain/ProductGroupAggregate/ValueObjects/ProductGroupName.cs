using Ardalis.GuardClauses;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Exceptions;

namespace CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
public class ProductGroupName : ValueObject
{
    public string Value { get; private set; } = null!;
    public ProductGroupName(string name)
    {
        Value = Guard.Against.NullOrWhiteSpace(
            name,
            exceptionCreator: () => ProductGroupException.Create(ProductGroupError.InvalidName));
    }

    private ProductGroupName()
    {

    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductGroupName productGroupName) => productGroupName.Value;
}
