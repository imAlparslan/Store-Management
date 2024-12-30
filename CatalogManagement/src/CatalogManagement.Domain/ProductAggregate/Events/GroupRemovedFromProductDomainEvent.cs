using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductAggregate.Events;
public record GroupRemovedFromProductDomainEvent(Guid GroupId, Guid ProductId) : IDomainEvent;
