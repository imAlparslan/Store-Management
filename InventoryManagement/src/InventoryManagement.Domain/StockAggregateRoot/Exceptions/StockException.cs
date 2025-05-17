
namespace InventoryManagement.Domain.StockAggregateRoot.Exceptions;
public class StockException : DomainException
{
    private StockException(string code, string? message) : base(code, message)
    {
    }

    public static StockException Create(Error error)
        => new StockException(error.Code, error.Description);

}
