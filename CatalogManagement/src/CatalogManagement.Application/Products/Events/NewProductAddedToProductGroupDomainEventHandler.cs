using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate.Events;

namespace CatalogManagement.Application.Products.Events;
internal class NewProductAddedToProductGroupDomainEventHandler(IProductRepository productRepository) : IDomainEventHandler<NewProductAddedToProductGroupDomainEvent>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task Handle(NewProductAddedToProductGroupDomainEvent notification, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(notification.ProductId,cancellationToken);
        if (product is not null)
        {
            product.AddGroup(notification.ProductGroupId);
            await productRepository.UpdateAsync(product, cancellationToken);
        }
    }
}
