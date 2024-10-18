using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Queries.GetProductById;
public record GetProductByIdQuery(Guid Id) : IRequest<Result<Product>>;
