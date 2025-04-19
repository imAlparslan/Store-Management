using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.GroupAggregateRoot.Events;
public sealed record GroupDeletedDomainEvent(GroupId GroupId) : IDomainEvent;
