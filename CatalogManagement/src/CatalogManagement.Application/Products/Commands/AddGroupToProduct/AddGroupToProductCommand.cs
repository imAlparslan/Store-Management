using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Application.Products.Commands.AddGroup;
public sealed record AddGroupToProductCommand(Guid ProductId, Guid GroupId)
    : ICommand<Result<Product>>;