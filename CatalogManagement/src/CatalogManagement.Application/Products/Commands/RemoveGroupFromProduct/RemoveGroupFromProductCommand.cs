using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;
public record RemoveGroupFromProductCommand(Guid GroupId, Guid ProductId)
    : IRequest<Result<Product>>;
