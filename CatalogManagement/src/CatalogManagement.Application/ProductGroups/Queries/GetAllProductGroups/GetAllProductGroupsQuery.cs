using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.ProductGroups;
public sealed record GetAllProductGroupsQuery()
    : IQuery<Result<IEnumerable<ProductGroup>>>;
