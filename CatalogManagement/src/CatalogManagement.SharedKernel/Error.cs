namespace CatalogManagement.SharedKernel;
public class Error
{
    public string Code { get; }
    public string? Description { get; }

    public ErrorType Type { get; }

    public Error(string code, ErrorType type, string? description = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code, nameof(code));

        Code = code;
        Type = type;
        Description = description;
    }
}
