using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Events;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups.Commands.AddProduct;
internal sealed class AddProductToGroupCommandHandler(IProductGroupRepository productGroupRepository)
    : IRequestHandler<AddProductToGroupCommand, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;
    public async Task<Result<ProductGroup>> Handle(AddProductToGroupCommand request, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(request.ProductGroupId, cancellationToken);
        
        if (productGroup is null)
        {
            return ProductGroupError.NotFoundById;
        }

        productGroup.AddProduct(request.ProductId);
        productGroup.AddDomainEvent(new NewProductAddedToProductGroupDomainEvent(productGroup.Id, request.ProductId));
     
        return await productGroupRepository.UpdateAsync(productGroup, cancellationToken);
    }
}
