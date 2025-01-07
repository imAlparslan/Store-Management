using Ardalis.GuardClauses;
using StoreDefinition.Domain.Common.Models;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.Exceptions;

namespace StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
public sealed class ShopAddress : ValueObject
{
    //TODO: continue here
    public string City { get; private set; } = null!;
    public string Street { get; private set; } = null!;

    public ShopAddress(string city, string street)
    {
        City = Guard.Against
            .NullOrWhiteSpace(
                city,
                exceptionCreator: () => ShopException.Create(ShopErrors.InvalidCity));

        Street = Guard.Against
            .NullOrWhiteSpace(
                street,
                exceptionCreator: () => ShopException.Create(ShopErrors.InvalidStreet));
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Street;
    }
}
