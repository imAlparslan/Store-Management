
namespace StoreDefinition.Application.Tests.Common;

public class InvalidData
{
    public class InvalidGuids : TheoryData<Guid?>
    {
        public InvalidGuids()
        {
            AddRange(Guid.Empty, null);
        }
    }

    public class InvalidStrings : TheoryData<string>
    {
        public InvalidStrings()
        {
            AddRange("", " ", null!);
        }
    }
}
