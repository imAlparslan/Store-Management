using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductAggregate.Events;
public sealed record ProductDeletedDomainEvent(Guid ProductId) : IDomainEvent;
