using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products;
public sealed record GetAllProductsQuery
    : IQuery<Result<IEnumerable<Product>>>;
