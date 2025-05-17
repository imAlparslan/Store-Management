public class ItemErrors
{
    private ItemErrors()
    {
    }

    public static readonly Error InvalidProductName =
        new Error("Invalid.Item.InvalidProductName", ErrorType.Validation, "Given 'product name' is invalid.");

    public static readonly Error InvalidProductCode =
        new Error("Invalid.Item.InvalidProductCode", ErrorType.Validation, "Given 'product code' is invalid.");

    public static readonly Error InvalidProductDefinition =
        new Error("Invalid.Item.InvalidProductDefinition", ErrorType.Validation, "Given 'product definition' is invalid.");
}

