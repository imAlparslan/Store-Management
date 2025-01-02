using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductGroupAggregate.Events;
public sealed record NewProductAddedToProductGroupDomainEvent(Guid ProductGroupId, Guid ProductId) : IDomainEvent;
