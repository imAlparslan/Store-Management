using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductGroupAggregate.Events;
public record class ProductGroupDeletedDomainEvent(Guid ProductGroupId) : IDomainEvent;
