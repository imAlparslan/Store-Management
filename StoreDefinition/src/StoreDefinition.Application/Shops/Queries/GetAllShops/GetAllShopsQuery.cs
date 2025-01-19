using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Queries.GetAllShops;
public sealed record GetAllShopsQuery : IQuery<Result<IEnumerable<Shop>>>;