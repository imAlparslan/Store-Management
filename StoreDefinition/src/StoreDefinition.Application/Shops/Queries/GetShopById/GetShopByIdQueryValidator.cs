using FluentValidation;

namespace StoreDefinition.Application.Shops.Queries.GetShopById;
internal sealed class GetShopByIdQueryValidator : AbstractValidator<GetShopByIdQuery>
{
    public GetShopByIdQueryValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty();
    }
}
