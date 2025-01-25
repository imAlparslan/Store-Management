namespace StoreDefinition.Contracts.Shops;
public sealed record ShopResponse(Guid Id,
                                  string Description,
                                  string City,
                                  string Street,
                                  IReadOnlyList<Guid> Groups);
