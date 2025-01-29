namespace CatalogManagement.Application.Tests.Common.InvalidData;
public class InvalidGuidData : TheoryData<Guid>
{
    public InvalidGuidData()
    {
        Add(Guid.Empty);
        Add(default);
    }
}
