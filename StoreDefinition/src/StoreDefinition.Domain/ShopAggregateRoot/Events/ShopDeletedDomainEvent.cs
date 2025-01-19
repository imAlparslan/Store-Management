using StoreDefinition.Domain.Common.Interfaces;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.ShopAggregateRoot.Events;
public sealed record ShopDeletedDomainEvent
    (ShopId ShopId) : IDomainEvent;
