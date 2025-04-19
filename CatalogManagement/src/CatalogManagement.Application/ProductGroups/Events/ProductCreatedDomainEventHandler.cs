using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.ProductGroups.Events;
internal sealed class ProductCreatedDomainEventHandler(IProductGroupRepository productGroupRepository) : IDomainEventHandler<ProductCreatedDomainEvent>
{
    private readonly IProductGroupRepository _productGroupRepository = productGroupRepository;
    public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var groups = await _productGroupRepository.GetByIdsAsync(notification.Product.GroupIds, cancellationToken);

        groups.ForEach(group => group.AddProduct(notification.Product.Id));

        await _productGroupRepository.UpdateRangeAsync(groups, cancellationToken);
    }
}
