using CatalogManagement.Application.Common.Behaviors;
using CatalogManagement.Domain.ProductAggregate;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute.Extensions;

namespace CatalogManagement.Application.Tests.Behaviors;

public class CommandValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<Product>> nextBehavior;
    private readonly IValidator<CreateProductCommand> mockValidator;
    private readonly List<IValidator<CreateProductCommand>> mockValidators;

    public CommandValidationBehaviorTests()
    {
        nextBehavior = Substitute.For<RequestHandlerDelegate<Result<Product>>>();
        mockValidator = Substitute.For<IValidator<CreateProductCommand>>();
        mockValidators = Substitute.For<List<IValidator<CreateProductCommand>>>();
    }


    [Fact]
    public async Task InvokedBehavior_ShouldInvokesNextBehavior_WhenValidatorResultValid()
    {
        //Arrange
        var command = CreateProductCommandFactory.CreateValid();
        var product = ProductFactory.CreateDefault();

        nextBehavior.Invoke().Returns(product);
        mockValidator.ReturnsForAll(Task.FromResult(new ValidationResult()));
        mockValidators.Add(mockValidator);

        CommandValidationBehavior<CreateProductCommand, Result<Product>> commandValidationBehavior = new(mockValidators);

        //Act
        var result = await commandValidationBehavior.Handle(command, nextBehavior, default);

        //Assert
        result.IsSuccess.ShouldBeTrue();
        result.Errors.ShouldBeNull();

    }

    [Fact]
    public async Task InvolkeBehavior_ReturnsValidationErrors_WhenValidatorResultNotValid()
    {
        //Arrange
        var command = CreateProductCommandFactory.CreateValid();
        var product = ProductFactory.CreateDefault();
        List<ValidationFailure> fails = [new("propname", "msg")];

        nextBehavior.Invoke().Returns(product);
        mockValidator.ReturnsForAll(Task.FromResult(new ValidationResult(fails)));
        mockValidators.Add(mockValidator);

        CommandValidationBehavior<CreateProductCommand, Result<Product>> commandValidationBehavior = new(mockValidators);

        //Act
        var result = await commandValidationBehavior.Handle(command, nextBehavior, default);

        //Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }
}
