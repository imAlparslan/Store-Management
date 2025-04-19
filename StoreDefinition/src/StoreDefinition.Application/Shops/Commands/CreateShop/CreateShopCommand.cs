using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Shops.Commands.CreateShop;
public sealed record CreateShopCommand(string Description,
                                       string City,
                                       string Street,
                                       IReadOnlyList<Guid> GroupIds) : ICommand<Result<Shop>>;