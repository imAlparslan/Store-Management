using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Events;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups.Commands.RemoveProductFromProductGroup;
internal sealed class RemoveProductFromProductGroupCommandHandler(IProductGroupRepository productGroupRepository)
        : IRequestHandler<RemoveProductFromProductGroupCommand, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<ProductGroup>> Handle(RemoveProductFromProductGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await productGroupRepository.GetByIdAsync(request.ProductGroupId, cancellationToken);
        if (group is null)
        {
            return ProductGroupError.NotFoundById;
        }

        group.RemoveProduct(request.ProductId);
        group.AddDomainEvent(new ProductRemovedFromProductGroupDomainEvent(group.Id, request.ProductId));
        return await productGroupRepository.UpdateAsync(group, cancellationToken);
    }
}
