using InventoryManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.MsSql;

namespace InventoryManagement.Api.Tests;


public class InventoryApiFactory : WebApplicationFactory<IApiAssemblyMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer msSqlContainer;
    public HttpClient client { get; private set; } = default!;
    public InventoryApiFactory()
    {
        msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_PID", "Developer")
            .WithPassword("test_pWd")
            .Build();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(x => x.ClearProviders());

        builder.ConfigureTestServices(
            services =>
            {
                services.RemoveAll<DbContextOptions<InventoryDbContext>>();
                services.RemoveAll<InventoryDbContext>();


                var connectionStringBuilder = new SqlConnectionStringBuilder
                {
                    ConnectionString = msSqlContainer.GetConnectionString(),
                    InitialCatalog = "Api_Test"
                };

                services.AddDbContext<InventoryDbContext>((services, option) =>
                {
                    option.UseSqlServer(connectionString: connectionStringBuilder.ConnectionString);
                });
            }
        );
    }
    public async Task ResetDb()
    {
        var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();

    }
    public async Task InitializeAsync()
    {
        await msSqlContainer.StartAsync();
        client = CreateClient();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await msSqlContainer.DisposeAsync();
    }
}