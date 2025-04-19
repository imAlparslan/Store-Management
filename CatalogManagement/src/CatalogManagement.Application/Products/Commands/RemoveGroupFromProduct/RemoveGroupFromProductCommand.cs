using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;
public sealed record RemoveGroupFromProductCommand(Guid GroupId, Guid ProductId)
    : ICommand<Result<Product>>;
