using StoreDefinition.SharedKernel;

namespace StoreDefinition.Domain.GroupAggregateRoot.Errors;
public class GroupErrors
{
    public static readonly Error InvalidName = new Error(
       "Invalid.Group.GroupName",
       ErrorType.Validation,
       "Given group 'Name' is invalid.");

    public static readonly Error InvalidDescription = new Error(
        "Invalid.Group.GroupDescription",
        ErrorType.Validation,
        "Given group 'description' is invalid.");

}
