using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Events;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.ProductGroups.Commands.RemoveProductFromProductGroup;
internal sealed class RemoveProductFromProductGroupCommandHandler(IProductGroupRepository productGroupRepository)
        : ICommandHandler<RemoveProductFromProductGroupCommand, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<ProductGroup>> Handle(RemoveProductFromProductGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await productGroupRepository.GetByIdAsync(request.ProductGroupId, cancellationToken);
        if (group is null)
        {
            return ProductGroupError.NotFoundById;
        }

        var result = group.RemoveProduct(request.ProductId);

        if(result)
        {
            group.AddDomainEvent(new ProductRemovedFromProductGroupDomainEvent(group.Id, request.ProductId));
            return await productGroupRepository.UpdateAsync(group, cancellationToken);
        }

        return ProductGroupError.ProductNotRemovedFromProductGroup;

    }
}
