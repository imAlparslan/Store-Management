using CatalogManagement.Application.ProductGroups.Commands.AddProduct;
using CatalogManagement.Domain.ProductGroupAggregate.Events;

namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;
public class AddProductToGroupCommandHandlerTests
{
    [Fact]
    public async Task Handler_ReturnsProductGroup_WhenDataValid()
    {
        var productId = Guid.NewGuid();
        var productGroup = ProductGroupFactory.CreateDefault();
        var mockProductGroupRepository = Substitute.For<IProductGroupRepository>();
        mockProductGroupRepository.GetByIdAsync(default!).ReturnsForAnyArgs(productGroup);
        mockProductGroupRepository.UpdateAsync(default!).ReturnsForAnyArgs(productGroup);
        var handler = new AddProductToGroupCommandHandler(mockProductGroupRepository);
        var command = new AddProductToGroupCommand(productGroup.Id, productId);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
            result.Value!.GetDomainEvents().Should().HaveCount(1);
            result.Value!.GetDomainEvents().Should().ContainItemsAssignableTo<NewProductAddedToProductGroupDomainEvent>();
            result.Value!.ProductIds.Should().Contain(productId);
        }
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundById_WhenIdNotExists()
    {
        var mockProductGroupRepository = Substitute.For<IProductGroupRepository>();
        mockProductGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).ReturnsNull();
        var handler = new AddProductToGroupCommandHandler(mockProductGroupRepository);
        var command = new AddProductToGroupCommand(Guid.NewGuid(), Guid.NewGuid());
        var result = await handler.Handle(command, default);
        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductGroupError.NotFoundById);
        }
    }
}
