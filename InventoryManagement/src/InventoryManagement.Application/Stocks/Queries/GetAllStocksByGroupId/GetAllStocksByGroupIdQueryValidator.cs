using FluentValidation;

namespace InventoryManagement.Application.Stocks.Queries.GetStockByGroupId;

internal sealed class GetAllStocksByGroupIdQueryValidator : AbstractValidator<GetAllStocksByGroupIdQuery>
{
    public GetAllStocksByGroupIdQueryValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
    }
}
