using Ardalis.GuardClauses;
using StoreDefinition.Domain.Common.Models;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.Exceptions;

namespace StoreDefinition.Domain.ShopAggregateRoot.Entities;
public sealed class ShopAddress : Entity<ShopAddressId>
{
    //TODO: continue here
    public string City { get; private set; }
    public string Street { get; private set; }

    public ShopAddress(string city, string street, ShopAddressId? id = null) : base(id ?? ShopAddressId.CreateUnique())
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
}
