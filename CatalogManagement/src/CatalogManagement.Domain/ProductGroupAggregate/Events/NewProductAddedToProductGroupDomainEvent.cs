using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductGroupAggregate.Events;
public record NewProductAddedToProductGroupDomainEvent(Guid ProductGroupId, Guid ProductId) : IDomainEvent;
