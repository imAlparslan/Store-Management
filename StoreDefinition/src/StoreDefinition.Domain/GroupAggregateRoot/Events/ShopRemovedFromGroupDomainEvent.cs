using StoreDefinition.Domain.Common.Interfaces;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.GroupAggregateRoot.Events;
public sealed record ShopRemovedFromGroupDomainEvent(GroupId GroupId, ShopId ShopId) : IDomainEvent;
