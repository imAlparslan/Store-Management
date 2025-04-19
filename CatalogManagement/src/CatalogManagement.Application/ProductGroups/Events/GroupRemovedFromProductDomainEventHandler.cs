using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.ProductGroups.Events;
internal class GroupRemovedFromProductDomainEventHandler(IProductGroupRepository productGroupRepository) : IDomainEventHandler<GroupRemovedFromProductDomainEvent>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task Handle(GroupRemovedFromProductDomainEvent notification, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(notification.GroupId, cancellationToken);
        if (productGroup is not null)
        {
            productGroup.RemoveProduct(notification.ProductId);
            await productGroupRepository.UpdateAsync(productGroup, cancellationToken);
        }
    }
}
