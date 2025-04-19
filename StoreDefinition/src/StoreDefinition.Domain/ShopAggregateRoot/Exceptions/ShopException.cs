namespace StoreDefinition.Domain.ShopAggregateRoot.Exceptions;
public class ShopException : DomainException
{
    private ShopException(string code, string? message) : base(code, message)
    {
    }

    public static ShopException Create(Error error)
    {
        return new ShopException(error.Code, error.Description);
    }
}
