using FluentValidation;

namespace InventoryManagement.Application.Stocks.Queries.GetStockByStoreId;

public class GetStockByStoreIdQueryValidator : AbstractValidator<GetStockByStoreIdQuery>
{
    public GetStockByStoreIdQueryValidator()
    {
        RuleFor(x => x.StoreId).NotEmpty();
    }
}