namespace StoreDefinition.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class ShopsEndpoints
    {
        private const string ShopsBase = $"{ApiBase}/shops";

        public const string Create = ShopsBase;
        public const string Update = $"{ShopsBase}/{{id:guid}}";
        public const string Delete = $"{ShopsBase}/{{id:guid}}";
        public const string GetById = $"{ShopsBase}/{{id:guid}}";
        public const string GetAll = ShopsBase;
        public const string AddGroupToShop = $"{ShopsBase}/{{id:guid}}/add-group";
        public const string RemoveGroupFromShop = $"{ShopsBase}/{{id:guid}}/remove-group";
    }

    public static class GroupsEndpoints
    {
        private const string GroupsBase = $"{ApiBase}/groups";

        public const string Create = GroupsBase;
        public const string Update = $"{GroupsBase}/{{id:guid}}";
        public const string Delete = $"{GroupsBase}/{{id:guid}}";
        public const string GetById = $"{GroupsBase}/{{id:guid}}";
        public const string GetAll = GroupsBase;
        public const string AddShopToGroup = $"{GroupsBase}/{{id:guid}}/add-shop";
        public const string RemoveShopFromGroup = $"{GroupsBase}/{{id:guid}}/remove-shop";
    }
}
