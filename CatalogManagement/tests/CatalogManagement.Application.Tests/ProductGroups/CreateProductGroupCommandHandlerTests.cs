﻿using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Application.Tests.Common.Factories.CommandFactories;
using CatalogManagement.Application.Tests.Common.Factories.ProductGroupFactories;
using CatalogManagement.Domain.ProductGroupAggregate.Exceptions;
using NSubstitute;

namespace CatalogManagement.Application.Tests.ProductGroups;
public class CreateProductGroupCommandHandlerTests
{
    [Fact]
    public async void Handler_ReturnsSuccessResult_WhenDataValid()
    {
        var command = CreateProductGroupCommandFactory.CreateValid();
        var productGroup = ProductGroupFactory.CreateFromCreateCommand(command);
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.InsertAsync(default!).ReturnsForAnyArgs(productGroup);
        var handler = new CreateProductGroupCommandHandler(productGroupRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
            result.Value!.Name.Should().Be(productGroup.Name);
            result.Value!.Description.Should().Be(productGroup.Description);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Handler_ThrowsException_WhenProductGroupNameInvalid(string productGroupName)
    {
        var command = CreateProductGroupCommandFactory.CreateWithName(productGroupName);
        var handler = new CreateProductGroupCommandHandler(default!);

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
    public void Handler_ThrowsException_WhenProductGroupDewscriptionInvalid(string productGroupDescription)
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
