using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products;
public record UpdateProductCommand(Guid Id, string ProductName, string ProductCode, string ProductDefinition)
    : IRequest<Result<Product>>;

