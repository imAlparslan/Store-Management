using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.ProductGroups;
internal sealed class UpdateProductGroupCommandHandler(IProductGroupRepository productGroupRepository)
        : ICommandHandler<UpdateProductGroupCommand, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<ProductGroup>> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(request.Id, cancellationToken);
        if (productGroup is null)
        {
            return ProductGroupError.NotFoundById;
        }
        productGroup.ChangeName(new(request.Name));
        productGroup.ChangeDescription(new(request.Description));

        return await productGroupRepository.UpdateAsync(productGroup, cancellationToken);

    }
}
