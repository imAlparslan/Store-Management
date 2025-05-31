
namespace CatalogManagement.Domain.Tests.Common.InvalidData;

public class InvalidStringData: TheoryData<string>
{
    public InvalidStringData()
    {
        Add(string.Empty);
        Add("");
        Add(" ");
        Add(null!);
    }
}
