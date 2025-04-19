namespace SharedKernel.Domain.Common.Exceptions;
public class DomainException : Exception
{
    public string Code { get; }
    protected DomainException(string code, string? message) : base(message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        Code = code;
    }
}