using Ardalis.GuardClauses;
using CatalogManagement.Domain.Common.Models;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Exceptions;

namespace CatalogManagement.Domain.ProductAggregate.ValueObjects;
public class ProductCode : ValueObject
{
    public string Value { get; private set; }

    public ProductCode(string code)
    {
        Value = Guard.Against.NullOrWhiteSpace(
            code,
            exceptionCreator: () => ProductException.Create(ProductError.InvalidCode));
    }

    private ProductCode()
    {
        
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductCode code) => code.Value;
}
