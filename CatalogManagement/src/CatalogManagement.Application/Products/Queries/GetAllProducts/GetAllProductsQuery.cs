using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Application.Products;
public sealed record GetAllProductsQuery
    : IQuery<Result<IEnumerable<Product>>>;
