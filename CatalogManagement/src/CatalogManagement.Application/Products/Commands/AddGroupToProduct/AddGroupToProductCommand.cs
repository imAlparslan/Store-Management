using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products.Commands.AddGroup;
public sealed record AddGroupToProductCommand(Guid ProductId, Guid GroupId)
    : ICommand<Result<Product>>;