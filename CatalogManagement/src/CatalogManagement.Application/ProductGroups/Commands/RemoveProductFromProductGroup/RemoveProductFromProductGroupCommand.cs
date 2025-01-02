using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.ProductGroups.Commands.RemoveProductFromProductGroup;
public sealed record RemoveProductFromProductGroupCommand(Guid ProductGroupId, Guid ProductId)
    : ICommand<Result<ProductGroup>>;
