using FluentValidation;
using InventoryManagement.Domain.StockAggregateRoot.Errors;

namespace InventoryManagement.Application.Stocks.Commands.IncreaseStockCapacity;
internal class IncreaseStockItemCapacityCommandValidator : AbstractValidator<IncreaseStockItemCapacityCommand>
{

    public IncreaseStockItemCapacityCommandValidator()
    {
        RuleFor(x => x.StockItemId)
            .NotEmpty();

        RuleFor(x => x.StockId)
            .NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage(StockErrors.InvalidCapacityError.Description)
            .WithErrorCode(StockErrors.InvalidCapacityError.Code);
    }
}