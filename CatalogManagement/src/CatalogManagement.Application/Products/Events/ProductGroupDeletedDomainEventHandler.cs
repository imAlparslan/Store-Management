using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate.Events;
using MediatR;

namespace CatalogManagement.Application.Products.Events;
internal class ProductGroupDeletedDomainEventHandler(IProductRepository productRepository) : IDomainEventHandler<ProductGroupDeletedDomainEvent>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task Handle(ProductGroupDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetByGroupAsync(notification.ProductGroupId, cancellationToken);

        foreach (var product in products)
        {
            product.RemoveGroup(notification.ProductGroupId);
            await productRepository.UpdateAsync(product, cancellationToken);
        }
    }
}
