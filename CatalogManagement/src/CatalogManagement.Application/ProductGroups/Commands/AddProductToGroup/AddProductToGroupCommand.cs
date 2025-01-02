using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.ProductGroups.Commands.AddProduct;
public sealed record AddProductToGroupCommand(Guid ProductGroupId, Guid ProductId)
    : ICommand<Result<ProductGroup>>;