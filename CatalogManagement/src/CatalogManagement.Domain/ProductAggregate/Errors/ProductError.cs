using CatalogManagement.Domain.Common.Errors;

namespace CatalogManagement.Domain.ProductAggregate.Errors;
public class ProductError
{
    public static readonly Error InvalidName = new Error(
        "Invalid.Product.ProductName",
        "Given product 'name' is invalid.");

    public static readonly Error InvalidCode = new Error(
       "Invalid.Product.ProductCode",
       "Given product 'code' is invalid.");

    public static readonly Error InvalidDefinition = new Error(
       "Invalid.Product.ProductDefinition",
       "Given product 'definition' is invalid.");

}
