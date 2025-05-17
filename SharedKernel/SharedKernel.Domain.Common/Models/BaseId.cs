namespace SharedKernel.Domain.Common.Models;

public class BaseId : ValueObject
{
    public Guid Value { get; }

    protected BaseId(Guid value)
    {
        Value = value;
    }
    protected BaseId()
    {
        Value = Guid.CreateVersion7();
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}

