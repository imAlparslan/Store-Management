using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Commands.CreateProduct;
public record CreateProductCommand(string ProductName, string ProductCode, string ProductDefinition)
    : IRequest<Result<Product>>;
