using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products;
public record GetProductByIdQuery(Guid Id) : IRequest<Result<Product>>;
