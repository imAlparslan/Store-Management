using FluentValidation;

namespace StoreDefinition.Application.Shops.Queries.GetShopsByGroupId;
internal sealed class GetShopsByGroupIdQueryValidator : AbstractValidator<GetShopsByGroupIdQuery>
{
    public GetShopsByGroupIdQueryValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
    }
}
