using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductGroupAggregate.Events;
public record ProductRemovedFromProductGroupDomainEvent(Guid ProductGroupId, Guid ProductId) : IDomainEvent;
