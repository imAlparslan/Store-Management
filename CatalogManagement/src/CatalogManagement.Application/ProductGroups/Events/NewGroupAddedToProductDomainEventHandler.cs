using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.ProductGroups.Events;
internal class NewGroupAddedToProductDomainEventHandler(IProductGroupRepository productGroupRepository) : IDomainEventHandler<NewGroupAddedToProductDomainEvent>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;
    public async Task Handle(NewGroupAddedToProductDomainEvent notification, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(notification.GroupId, cancellationToken);
        if (productGroup is not null)
        {
            productGroup.AddProduct(notification.ProductId);
            await productGroupRepository.UpdateAsync(productGroup, cancellationToken);
        }
    }
}
