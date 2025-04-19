using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.ProductGroups.Events;
internal class ProductDeletedDomainEventHandler(IProductGroupRepository productGroupRepository) : IDomainEventHandler<ProductDeletedDomainEvent>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var groups = await productGroupRepository.GetProductGroupsByProductIdAsync(notification.ProductId, cancellationToken);
        //TODO: UNIT OF WORK ?
        foreach (var group in groups)
        {
            group.RemoveProduct(notification.ProductId);
            await productGroupRepository.UpdateAsync(group, cancellationToken);
        }
    }
}
