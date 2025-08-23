using FluentValidation;
using InventoryManagement.Application.Stocks.Commands.AddStockItem;
using InventoryManagement.Application.Tests.Factories.StockItemFactories;
using MediatR;
using SharedKernel.Common.Results;
using FluentValidation.Results;
using NSubstitute.Extensions;
using SharedKernel.Application.Common.Behaviors;
using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Application.Tests.Factories.StockFactories;

namespace InventoryManagement.Application.Tests.Behaviors;
public class CommandValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<Stock>> nextBehavior;
    private readonly IValidator<AddStockItemCommand> mockValidator;
    private readonly List<IValidator<AddStockItemCommand>> mockValidators;

    public CommandValidationBehaviorTests()
    {
        nextBehavior = Substitute.For<RequestHandlerDelegate<Result<Stock>>>();
        mockValidator = Substitute.For<IValidator<AddStockItemCommand>>();
        mockValidators = Substitute.For<List<IValidator<AddStockItemCommand>>>();
    }


    [Fact]
    public async Task InvolkeBehavior_ShouldInvolkeNextBehavior_WhenValidatorResultValid()
    {
        //Arrange
        var command = AddStockItemCommandFactory.CreateValid();
        var stock = StockFactory.CreateValid();

        nextBehavior.Invoke().Returns(stock);
        mockValidator.ReturnsForAll<Task<ValidationResult>>(Task.FromResult(new ValidationResult()));
        mockValidators.Add(mockValidator);

        CommandValidationBehavior<AddStockItemCommand, Result<Stock>> commandValidationBehavior = new(mockValidators);

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
        var command = AddStockItemCommandFactory.CreateValid();
        var stock = StockFactory.CreateValid();
        List<ValidationFailure> fails = [new("propname", "msg")];

        nextBehavior.Invoke().Returns(stock);
        mockValidator.ReturnsForAll<Task<ValidationResult>>(Task.FromResult(new ValidationResult(fails)));
        mockValidators.Add(mockValidator);

        CommandValidationBehavior<AddStockItemCommand, Result<Stock>> commandValidationBehavior = new(mockValidators);

        //Act
        var result = await commandValidationBehavior.Handle(command, nextBehavior, default);

        //Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
    }
}
