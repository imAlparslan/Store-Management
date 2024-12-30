using CatalogManagement.Application.ProductGroups.Commands.RemoveProductFromProductGroup;
using CatalogManagement.Domain.ProductGroupAggregate.Events;

namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;
public class RemoveProductFromProductGroupCommandHandlerTests
{
    [Fact]
    public async Task Handler_ReturnsTrue_WhenProductRemoved()
    {
        var productId = Guid.NewGuid();
        var productGroup = ProductGroupFactory.CreateDefault();
        productGroup.AddProduct(productId);
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetByIdAsync(default!).ReturnsForAnyArgs(productGroup);
        productGroupRepository.UpdateAsync(default!).ReturnsForAnyArgs(productGroup);
        var handler = new RemoveProductFromProductGroupCommandHandler(productGroupRepository);
        var command = new RemoveProductFromProductGroupCommand(productGroup.Id, productId);
        var result = await handler.Handle(command, default);
        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.GetDomainEvents().Should().HaveCount(1);
            result.Value.GetDomainEvents().Should().ContainItemsAssignableTo<ProductRemovedFromProductGroupDomainEvent>();
            result.Value.ProductIds.Should().NotContain(productId);
            result.Errors.Should().BeNullOrEmpty();

        }
    }
}
