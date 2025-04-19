namespace StoreDefinition.Application.Shops.Commands.DeleteShop;
public sealed record DeleteShopCommand(Guid ShopId) : ICommand<Result<bool>>;