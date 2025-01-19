using StoreDefinition.Domain.Common.Exceptions;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Domain.GroupAggregateRoot.Exceptions;
public class GroupException : DomainException
{
    private GroupException(string code, string? message) : base(code, message)
    {
    }

    public static GroupException Create(Error error)
    {
        return new GroupException(error.Code, error.Description);
    }
}