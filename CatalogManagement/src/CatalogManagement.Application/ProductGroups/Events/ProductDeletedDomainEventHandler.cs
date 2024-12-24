using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.ProductGroups.Events;
internal class ProductDeletedDomainEventHandler(IProductGroupRepository productGroupRepository) : IDomainEventHandler<ProductDeletedDomainEvent>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var groups = await productGroupRepository.GetProductGroupsByContainigProduct(notification.ProductId);

        foreach (var group in groups)
        {
            group.RemoveProduct(notification.ProductId);
            await productGroupRepository.UpdateAsync(group, cancellationToken);
        }
    }
}
