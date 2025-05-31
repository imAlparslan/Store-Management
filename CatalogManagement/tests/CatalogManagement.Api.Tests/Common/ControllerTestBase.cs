namespace CatalogManagement.Api.Tests.Common;
public abstract class ControllerTestBase : IAsyncLifetime
{
    protected readonly HttpClient _client;
    protected readonly Func<Task> _resetDatabase;
    protected const string ProductBaseAddress = "http://localhost/api/products";
    protected const string ProductGroupBaseAddress = "http://localhost/api/product-groups";

    protected ControllerTestBase(CatalogApiFactory apiFactory)
    {
        _client = apiFactory.Client;
        _resetDatabase = apiFactory.ResetDb;
    }

    public async Task InitializeAsync() => await _resetDatabase();

    public async Task DisposeAsync() => await Task.CompletedTask;
}
