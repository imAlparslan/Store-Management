using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Commands.AddGroup;
public record AddGroupToProductCommand(Guid ProductId, Guid GroupId) : IRequest<Result<Product>>;