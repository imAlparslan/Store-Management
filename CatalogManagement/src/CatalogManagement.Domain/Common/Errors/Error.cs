namespace CatalogManagement.Domain.Common.Errors;
public class Error
{
    public string Code { get; }
    public string? Description { get; }

    public Error(string code, string? description = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code, nameof(code));

        Code = code;
        Description = description;
    }
}
