namespace StoreDefinition.Domain.ShopAggregateRoot.Events;
public sealed record ShopCreatedDomainEvent(Shop Shop) : IDomainEvent;