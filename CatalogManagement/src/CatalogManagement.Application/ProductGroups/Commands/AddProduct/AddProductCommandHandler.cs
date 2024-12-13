using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups.Commands.AddProduct;
internal class AddProductCommandHandler(IProductGroupRepository productGroupRepository)
    : IRequestHandler<AddProductCommand, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;
    public async Task<Result<ProductGroup>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(request.ProductGroupId, cancellationToken);

        if (productGroup is null)
        {
            return ProductGroupError.NotFoundById;
        }

        productGroup.AddProduct(request.ProductId);

        await productGroupRepository.UpdateAsync(productGroup, cancellationToken);

        return productGroup;
    }
}
