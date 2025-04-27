using StoreDefinitionProtos;

namespace InventoryManagement.Infrastructure.ProtoHelpers;
public static class ProtoExtensions
{
    public static Id ToProto(this Guid guid)
    {
        return new Id() { Value = guid.ToString() };
    }

    public static Guid ToGuid(this Id id)
    {
        return Guid.Parse(id.Value);
    }
}
