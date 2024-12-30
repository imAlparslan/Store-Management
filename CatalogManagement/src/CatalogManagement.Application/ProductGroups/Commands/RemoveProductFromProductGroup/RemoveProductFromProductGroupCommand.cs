using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups.Commands.RemoveProductFromProductGroup;
public sealed record RemoveProductFromProductGroupCommand(Guid ProductGroupId, Guid ProductId)
    : IRequest<Result<ProductGroup>>;
