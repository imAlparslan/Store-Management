using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.ProductGroups;
public sealed record GetProductGroupByIdQuery(Guid Id)
    : IQuery<Result<ProductGroup>>;
