namespace InventoryManagement.Domain.StockAggregateRoot.Errors;

public class StockErrors
{
    private StockErrors()
    {
    }
    public static readonly Error StockItemNotFound =
        new Error("Invalid.Stock.StockItemNotFound", ErrorType.NotFound, "Given 'stock item' not found.");

    public static readonly Error StockGroupNotFound =
        new Error("Invalid.Stock.StockItemNotFound", ErrorType.NotFound, "Given 'stock group' not found.");

    public static readonly Error InvalidQuantityError =
        new Error("Invalid.Stock.Quantity", ErrorType.Validation, "Given 'quantity' is invalid.");

    public static readonly Error InsufficientStockError =
        new Error("Invalid.Stock.InsufficientStock", ErrorType.Validation, "Insufficient stock available for the operation.");
    public static readonly Error InvalidCapacityError =
        new Error("Invalid.Stock.Capacity", ErrorType.Validation, "Given 'capacity' is invalid.");

    public static readonly Error InsufficientCapacityError =
        new Error("Invalid.Stock.InsufficientCapacity", ErrorType.Validation, "Insufficient capacity available for the operation.");

}