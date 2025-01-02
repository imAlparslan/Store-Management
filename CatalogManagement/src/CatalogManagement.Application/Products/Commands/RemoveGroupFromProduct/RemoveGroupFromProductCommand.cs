using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;
public sealed record RemoveGroupFromProductCommand(Guid GroupId, Guid ProductId)
    : ICommand<Result<Product>>;
