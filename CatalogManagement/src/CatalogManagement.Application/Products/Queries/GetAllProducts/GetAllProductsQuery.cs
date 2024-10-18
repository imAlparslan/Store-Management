using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products;
public record GetAllProductsQuery : IRequest<Result<IEnumerable<Product>>>;
