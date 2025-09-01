using FluentValidation;

namespace InventoryManagement.Application.Stocks.Queries.GetStockById;

public class GetStockByIdQueryValidator : AbstractValidator<GetStockByIdQuery>
{
    public GetStockByIdQueryValidator()
    {
        RuleFor(x => x.StockId).NotEmpty();
    }
}
