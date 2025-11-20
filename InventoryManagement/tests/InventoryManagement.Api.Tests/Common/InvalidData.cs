namespace InventoryManagement.Api.Tests.Common;

public class InvalidData
{
    public class InvalidGuidData : TheoryData<string>
    {
        public InvalidGuidData()
        {
            Add(Guid.Empty.ToString());
        }
    }
}
