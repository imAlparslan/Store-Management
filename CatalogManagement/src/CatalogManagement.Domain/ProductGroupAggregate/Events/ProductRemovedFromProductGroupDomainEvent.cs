using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductGroupAggregate.Events;
public sealed record ProductRemovedFromProductGroupDomainEvent(Guid ProductGroupId, Guid ProductId) : IDomainEvent;
