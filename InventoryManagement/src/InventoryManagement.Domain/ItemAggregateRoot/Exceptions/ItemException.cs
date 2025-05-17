namespace InventoryManagement.Domain.ItemAggregateRoot.Exceptions;
public class ItemException : DomainException
{
    protected ItemException(string code, string? message) : base(code, message)
    {
    }

    public static ItemException Create(Error error)
        => new ItemException(error.Code, error.Description);
}
