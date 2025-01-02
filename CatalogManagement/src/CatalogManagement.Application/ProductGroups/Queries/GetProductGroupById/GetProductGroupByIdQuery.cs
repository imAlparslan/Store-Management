using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.ProductGroups;
public sealed record GetProductGroupByIdQuery(Guid Id)
    : IQuery<Result<ProductGroup>>;
