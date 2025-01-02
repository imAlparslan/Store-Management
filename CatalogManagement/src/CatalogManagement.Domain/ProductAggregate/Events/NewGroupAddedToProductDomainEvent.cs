using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductAggregate.Events;
public sealed record NewGroupAddedToProductDomainEvent(Guid GroupId, Guid ProductId) : IDomainEvent;
