namespace CatalogManagement.Application.Tests.Common.InvalidData;
public class InvalidStringData : TheoryData<string>
{
    public InvalidStringData()
    {
        Add(string.Empty);
        Add("");
        Add(" ");
        Add(null!);
    }
}
