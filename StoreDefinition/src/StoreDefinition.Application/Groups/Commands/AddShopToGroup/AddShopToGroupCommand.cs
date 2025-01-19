using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Commands.AddShopToGroup;
public sealed record AddShopToGroupCommand(Guid GroupId, Guid ShopId) : ICommand<Result<Group>>;
