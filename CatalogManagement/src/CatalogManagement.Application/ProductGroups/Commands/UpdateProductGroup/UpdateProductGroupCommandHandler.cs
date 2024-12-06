using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
internal class UpdateProductGroupCommandHandler
    : IRequestHandler<UpdateProductGroupCommand, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository;

    public UpdateProductGroupCommandHandler(IProductGroupRepository productGroupRepository)
    {
        this.productGroupRepository = productGroupRepository;
    }

    public async Task<Result<ProductGroup>> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(request.Id, cancellationToken);
        if (productGroup is null)
        {
            return Result<ProductGroup>.Fail(ProductGroupError.NotFoundById);
        }
        productGroup.ChangeName(new(request.Name));
        productGroup.ChangeDescription(new(request.Description));

        await productGroupRepository.UpdateAsync(productGroup, cancellationToken);

        return Result<ProductGroup>.Success(productGroup);
    }
}
