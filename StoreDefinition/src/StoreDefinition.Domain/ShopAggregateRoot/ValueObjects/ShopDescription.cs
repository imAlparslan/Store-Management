using Ardalis.GuardClauses;
using StoreDefinition.Domain.Common.Models;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.Exceptions;

namespace StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
public sealed class ShopDescription : ValueObject
{
    public string Value { get; private set; } = null!;

    public ShopDescription(string value)
    {
        Value = Guard.Against
            .NullOrWhiteSpace(
                value,
                exceptionCreator: () => ShopException.Create(ShopErrors.InvalidDescription));
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
