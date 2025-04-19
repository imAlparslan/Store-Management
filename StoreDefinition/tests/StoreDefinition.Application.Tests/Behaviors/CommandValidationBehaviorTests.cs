using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using NSubstitute.Extensions;
using SharedKernel.Common.Results;
using StoreDefinition.Application.Common.Behaviors;
using StoreDefinition.Application.Shops.Commands.CreateShop;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Tests.Behaviors;
public class CommandValidationBehaviorTests

{
    private readonly RequestHandlerDelegate<Result<Shop>> nextBehavior;
    private readonly IValidator<CreateShopCommand> mockValidator;
    private readonly List<IValidator<CreateShopCommand>> mockValidators;

    public CommandValidationBehaviorTests()
    {
        nextBehavior = Substitute.For<RequestHandlerDelegate<Result<Shop>>>();
        mockValidator = Substitute.For<IValidator<CreateShopCommand>>();
        mockValidators = Substitute.For<List<IValidator<CreateShopCommand>>>();
    }


    [Fact]
    public async Task InvolkeBehavior_ShouldInvolkeNextBehavior_WhenValidatorResultValid()
    {
        //Arrange
        var command = CreateShopCommandFactory.CreateValid();
        var shop = ShopFactory.CreateValid();

        nextBehavior.Invoke().Returns(shop);
        mockValidator.ReturnsForAll<Task<ValidationResult>>(Task.FromResult(new ValidationResult()));
        mockValidators.Add(mockValidator);

        CommandValidationBehavior<CreateShopCommand, Result<Shop>> commandValidationBehavior = new(mockValidators);

        //Act
        var result = await commandValidationBehavior.Handle(command, nextBehavior, default);

        //Assert
        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }

    }

    [Fact]
    public async Task InvolkeBehavior_ReturnsValidationErrors_WhenValidatorResultNotValid()
    {
        //Arrange
        var command = CreateShopCommandFactory.CreateValid();
        var shop = ShopFactory.CreateValid();
        List<ValidationFailure> fails = [new("propname", "msg")];

        nextBehavior.Invoke().Returns(shop);
        mockValidator.ReturnsForAll<Task<ValidationResult>>(Task.FromResult(new ValidationResult(fails)));
        mockValidators.Add(mockValidator);

        CommandValidationBehavior<CreateShopCommand, Result<Shop>> commandValidationBehavior = new(mockValidators);

        //Act
        var result = await commandValidationBehavior.Handle(command, nextBehavior, default);

        //Assert
        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
        }
    }
}
