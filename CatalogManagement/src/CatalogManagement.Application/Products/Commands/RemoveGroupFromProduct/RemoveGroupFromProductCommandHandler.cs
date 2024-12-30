using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Events;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;
internal class RemoveGroupFromProductCommandHandler(IProductRepository productRepository) : IRequestHandler<RemoveGroupFromProductCommand, Result<Product>>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<Result<Product>> Handle(RemoveGroupFromProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return ProductError.NotFoundById;
        }
        product.RemoveGroup(request.GroupId);
        product.AddDomainEvent(new GroupRemovedFromProductDomainEvent(request.GroupId, request.ProductId));
        return await productRepository.UpdateAsync(product, cancellationToken);
    }
}
