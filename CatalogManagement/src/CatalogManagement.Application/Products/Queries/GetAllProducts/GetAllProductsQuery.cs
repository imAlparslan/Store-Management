using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Queries.GetAllProducts;
public record GetAllProductsQuery : IRequest<Result<IEnumerable<Product>>>;
