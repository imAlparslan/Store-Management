﻿namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;
public class UpdateProductGroupCommandHandlerTests
{
    [Fact]
    public async Task Handler_ReturnsProductGroup_WhenDataValid()
    {
        var productGroup = ProductGroupFactory.CreateDefault();
        var command = UpdateProductGroupCommandFactory.CreateValid();
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetByIdAsync(default!).ReturnsForAnyArgs(productGroup);
        productGroupRepository.UpdateAsync(default!).ReturnsForAnyArgs(productGroup);
        var handler = new UpdateProductGroupCommandHandler(productGroupRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
            result.Value!.Name.Value.Should().Be(command.Name);
            result.Value!.Description.Value.Should().Be(command.Description);
        }
    }

    [Fact]
    public async Task Handler_ReturnsProductGroupError_WhenIdNotExists()
    {
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).ReturnsNull();
        var handler = new UpdateProductGroupCommandHandler(productGroupRepository);
        var command = UpdateProductGroupCommandFactory.CreateValid();

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess!.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductGroupError.NotFoundById);
        }


    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Handler_ThrowsException_WhenProductGroupNameInvalid(string productGroupName)
    {
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).Returns(ProductGroupFactory.CreateDefault());
        var command = UpdateProductGroupCommandFactory.CreateWithName(productGroupName);
        var handler = new UpdateProductGroupCommandHandler(productGroupRepository);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductGroupException>();
        }
    }


    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Handler_ThrowsException_WhenProductGroupDescriptionInvalid(string productGroupDescription)
    {
        var command = CreateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);
        var handler = new CreateProductGroupCommandHandler(default!);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductGroupException>();
        }
    }
}
