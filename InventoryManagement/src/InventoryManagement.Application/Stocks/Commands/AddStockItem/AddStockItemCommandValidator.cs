using FluentValidation;
using InventoryManagement.Domain.StockAggregateRoot.Errors;

namespace InventoryManagement.Application.Stocks.Commands.AddStockItem;
internal class AddStockItemCommandValidator : AbstractValidator<AddStockItemCommand>
{
    public AddStockItemCommandValidator()
    {
        RuleFor(x => x.StockId).NotEmpty();

        RuleFor(x => x.ItemId).NotEmpty();

        RuleFor(x => x).CapacityMustGreaterThanQuantity()
            .OverridePropertyName(x => x.InitialCapacity)
            .WithMessage(StockErrors.InsufficientCapacityError.Description)
            .WithErrorCode(StockErrors.InsufficientCapacityError.Code);

        RuleFor(x => x.InitialQuantity).GreaterThanOrEqualTo(0)
            .WithMessage(StockErrors.InvalidQuantityError.Description)
            .WithErrorCode(StockErrors.InvalidQuantityError.Code);

        RuleFor(x => x.InitialCapacity).GreaterThanOrEqualTo(0)
            .WithMessage(StockErrors.InvalidCapacityError.Description)
            .WithErrorCode(StockErrors.InvalidCapacityError.Code);

    }
}

public static class CapacityOverflowValidation
{
    public static IRuleBuilderOptions<AddStockItemCommand, AddStockItemCommand> CapacityMustGreaterThanQuantity(this IRuleBuilder<AddStockItemCommand, AddStockItemCommand> ruleBuilder)
    {
        return ruleBuilder.Must(x => x.InitialCapacity >= x.InitialQuantity);
    }
}