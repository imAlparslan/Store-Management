using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate.Events;

namespace CatalogManagement.Application.Products.Events;
internal class ProductRemovedFromProductGroupDomainEventHandler(IProductRepository productRepository) : IDomainEventHandler<ProductRemovedFromProductGroupDomainEvent>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task Handle(ProductRemovedFromProductGroupDomainEvent notification, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(notification.ProductId, cancellationToken);
        if (product is not null)
        {
            product.RemoveGroup(notification.ProductGroupId);
            await productRepository.UpdateAsync(product, cancellationToken);
        }
    }
}
