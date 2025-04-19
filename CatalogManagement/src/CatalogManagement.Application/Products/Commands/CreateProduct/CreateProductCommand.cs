using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Application.Products;
public sealed record CreateProductCommand(string ProductName,
                                          string ProductCode,
                                          string ProductDefinition,
                                          IReadOnlyList<Guid> GroupIds)
                                        : ICommand<Result<Product>>;
