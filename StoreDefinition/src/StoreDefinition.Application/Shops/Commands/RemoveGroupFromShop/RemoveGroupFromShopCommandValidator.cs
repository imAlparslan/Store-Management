using FluentValidation;

namespace StoreDefinition.Application.Shops.Commands.RemoveGroupFromShop;
internal sealed class RemoveGroupFromShopCommandValidator : AbstractValidator<RemoveGroupFromShopCommand>
{
    public RemoveGroupFromShopCommandValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty();
        RuleFor(x => x.GroupId).NotEmpty();
    }
}
