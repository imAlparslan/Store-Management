using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.ProductGroups;
public sealed record GetAllProductGroupsQuery()
    : IQuery<Result<IEnumerable<ProductGroup>>>;
