namespace InventoryManagement.Api;

public static class ApiEndPoints
{
    private const string ApiBase = "api";
    public static class StocksEndpoints
    {
        private const string StocksBase = $"{ApiBase}/stocks";
        public const string GetAllStocksByGroupId = $"{StocksBase}/by-group/";
    }
}
