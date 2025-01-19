namespace StoreDefinition.SharedKernel;
public class Error
{
    public string Code { get; }
    public string Description { get; }

    public Dictionary<string, object>? MetaData;
    public ErrorType Type { get; }

    public Error(string code, ErrorType type, string description, Dictionary<string, object>? metaData = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code, nameof(code));
        ArgumentException.ThrowIfNullOrWhiteSpace(description, nameof(description));

        Code = code;
        Type = type;
        Description = description;
        MetaData = metaData;
    }
}
