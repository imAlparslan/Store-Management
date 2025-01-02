using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductGroupAggregate.Events;
public sealed record class ProductGroupDeletedDomainEvent(Guid ProductGroupId) : IDomainEvent;
