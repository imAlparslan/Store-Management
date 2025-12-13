namespace InventoryManagement.Application.Tests.Common;

public class InvalidGuidData : TheoryData<string>
{
    public InvalidGuidData()
    {
        Add(Guid.Empty.ToString());
    }
}
