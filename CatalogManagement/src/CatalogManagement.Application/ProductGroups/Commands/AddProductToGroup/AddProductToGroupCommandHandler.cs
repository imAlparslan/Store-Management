using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Events;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.ProductGroups.Commands.AddProduct;
internal sealed class AddProductToGroupCommandHandler(IProductGroupRepository productGroupRepository)
    : ICommandHandler<AddProductToGroupCommand, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;
    public async Task<Result<ProductGroup>> Handle(AddProductToGroupCommand request, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(request.ProductGroupId, cancellationToken);

        if (productGroup is null)
        {
            return ProductGroupError.NotFoundById;
        }

        var result = productGroup.AddProduct(request.ProductId);

        if (result)
        {
            productGroup.AddDomainEvent(new NewProductAddedToProductGroupDomainEvent(productGroup.Id, request.ProductId));
            return await productGroupRepository.UpdateAsync(productGroup, cancellationToken);
        }

        return ProductGroupError.ProductNotAddedToProductGroup;
    }
}
