using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductAggregate.Events;
public sealed record GroupRemovedFromProductDomainEvent(Guid GroupId, Guid ProductId) : IDomainEvent;
