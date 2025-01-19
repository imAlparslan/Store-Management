using FluentValidation;

namespace StoreDefinition.Application.Groups.Queries.GetGroupsByShopId;
internal sealed class GetGroupsByShopIdQueryValidator : AbstractValidator<GetGroupsByShopIdQuery>
{
    public GetGroupsByShopIdQueryValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty();
    }
}
