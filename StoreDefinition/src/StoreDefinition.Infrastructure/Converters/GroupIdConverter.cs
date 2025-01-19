using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Infrastructure.Converters;
internal class GroupIdConverter : ValueConverter<GroupId, Guid>
{
    public GroupIdConverter() : base(
            GroupId => GroupId.Value,
            Guid => Guid)
    {
    }
}
