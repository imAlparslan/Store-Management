using CatalogManagement.Domain.Common.Errors;

namespace CatalogManagement.Domain.ProductGroupAggregate.Errors;
public static class ProductGroupError
{
    public static readonly Error InvalidName = new Error(
        "Invalid.ProductGroup.ProductGroupName",
        "Given product group 'name' is invalid.");

    public static readonly Error InvalidDescription = new Error(
        "Invalid.ProductGroup.ProductGroupDescription",
        "Given product group 'description' is invalid.");
}
