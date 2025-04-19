using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.ShopAggregateRoot.Events;
public sealed record GroupRemovedFromShopDomainEvent
    (ShopId ShopId, GroupId GroupId) : IDomainEvent;
