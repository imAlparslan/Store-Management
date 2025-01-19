using StoreDefinition.SharedKernel;

namespace StoreDefinition.Domain.GroupAggregateRoot.Errors;
public class GroupErrors
{
    private GroupErrors()
    {
        
    }
    public static readonly Error InvalidName = new Error(
       "Invalid.Group.GroupName",
       ErrorType.Validation,
       "Given group 'Name' is invalid.");

    public static readonly Error InvalidDescription = new Error(
        "Invalid.Group.GroupDescription",
        ErrorType.Validation,
        "Given group 'description' is invalid.");

    public static readonly Error NotFoundById = new Error(
       "Invalid.Group.NotFoundById",
       ErrorType.NotFound,
       "Group does not found.");

    public static readonly Error ShopNotAddedToGroup = new Error(
       "Invalid.Group.ShopNotAddedToGroup",
       ErrorType.NotUpdated,
       "Shop does not added to group.");

    public static readonly Error ShopNotRemovedFromGroup = new Error(
       "Invalid.Group.ShopNotRemovedFromGroup",
       ErrorType.NotUpdated,
       "Shop does not removed from group.");
}
