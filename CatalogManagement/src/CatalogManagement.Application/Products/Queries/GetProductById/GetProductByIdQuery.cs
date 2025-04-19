using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Application.Products;
public sealed record GetProductByIdQuery(Guid Id)
    : IQuery<Result<Product>>;
