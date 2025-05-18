namespace InventoryManagement.Domain.Tests.Common;
public class InvalidData
{
    public class InvalidString : TheoryData<string>
    {
        public InvalidString() => AddRange("", " ", null!);

    }


}

