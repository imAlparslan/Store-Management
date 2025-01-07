namespace StoreDefinition.Domain.Common.Exceptions;
public class DomainException : Exception
{
    public string Code { get; init; }

    public DomainException(string code, string? message) : base(message)
    {
        ArgumentNullException.ThrowIfNull(code, nameof(code));

        Code = code;
    }
}
