using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductAggregate.Events;
public record ProductDeletedDomainEvent(Guid ProductId) : IDomainEvent;
