using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products;
public sealed record CreateProductCommand(string ProductName,
                                          string ProductCode,
                                          string ProductDefinition,
                                          IReadOnlyList<Guid> GroupIds)
                                        : ICommand<Result<Product>>;
