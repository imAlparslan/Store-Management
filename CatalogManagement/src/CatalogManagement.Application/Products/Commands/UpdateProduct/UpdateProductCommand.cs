using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Application.Products;
public sealed record UpdateProductCommand(Guid Id, string ProductName, string ProductCode, string ProductDefinition)
    : ICommand<Result<Product>>;

