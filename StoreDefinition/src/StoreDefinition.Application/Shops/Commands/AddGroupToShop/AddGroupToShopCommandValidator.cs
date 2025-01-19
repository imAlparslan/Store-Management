using FluentValidation;

namespace StoreDefinition.Application.Shops.Commands.AddGroupToShop;
internal sealed class AddGroupToShopCommandValidator : AbstractValidator<AddGroupToShopCommand>
{
    public AddGroupToShopCommandValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty();
        RuleFor(x => x.GroupId).NotEmpty();
    }
}
