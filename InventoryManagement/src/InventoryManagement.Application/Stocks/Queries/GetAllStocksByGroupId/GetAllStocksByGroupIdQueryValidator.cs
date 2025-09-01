using FluentValidation;

namespace InventoryManagement.Application.Stocks.Queries.GetAllStocksByGroupId;

public class GetAllStocksByGroupIdQueryValidator : AbstractValidator<GetAllStocksByGroupIdQuery>
{
    public GetAllStocksByGroupIdQueryValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
    }
}
