using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Events;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;
internal sealed class RemoveGroupFromProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<RemoveGroupFromProductCommand, Result<Product>>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<Result<Product>> Handle(RemoveGroupFromProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return ProductError.NotFoundById;
        }

        var result = product.RemoveGroup(request.GroupId);

        if(result)
        {
            return await productRepository.UpdateAsync(product, cancellationToken);
        }

        return ProductError.ProductGroupNotDeletedFromProduct;
    }
}
