using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Commands.RemoveShopFromGroup;
public sealed record RemoveShopFromGroupCommand(Guid GroupId, Guid ShopId) : ICommand<Result<Group>>;
