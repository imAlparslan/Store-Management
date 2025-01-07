using StoreDefinition.Domain.Common.Interfaces;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.ShopAggregateRoot.Events;
public sealed record GroupAddedToShopDomainEvent
    (ShopId ShopId, GroupId GroupId) : IDomainEvent;
