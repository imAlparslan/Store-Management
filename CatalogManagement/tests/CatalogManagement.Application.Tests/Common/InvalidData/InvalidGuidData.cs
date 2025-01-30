namespace CatalogManagement.Application.Tests.Common.InvalidData;
public class InvalidGuidData : TheoryData<string>
{
    public InvalidGuidData()
    {
        Add(Guid.Empty.ToString());
    }
}
