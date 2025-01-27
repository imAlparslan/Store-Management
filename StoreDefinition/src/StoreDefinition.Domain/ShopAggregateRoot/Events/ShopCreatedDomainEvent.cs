using StoreDefinition.Domain.Common.Interfaces;

namespace StoreDefinition.Domain.ShopAggregateRoot.Events;
public sealed record ShopCreatedDomainEvent(Shop Shop) : IDomainEvent;