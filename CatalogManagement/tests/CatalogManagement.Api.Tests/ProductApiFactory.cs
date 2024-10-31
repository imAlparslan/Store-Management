using CatalogManagement.Api.Controllers;
using CatalogManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.MsSql;

namespace CatalogManagement.Api.Tests;

public class ProductApiFactory : WebApplicationFactory<ProductsController>, IAsyncLifetime
{
    private readonly MsSqlContainer _mssqlContainer =
        new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_PID", "Developer")
            .WithPassword("test_pWd")
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging => logging.ClearProviders());

        builder.ConfigureTestServices(
            services =>
            {
                services.RemoveAll(typeof(DbContextOptions<CatalogDbContext>));
                services.RemoveAll(typeof(CatalogDbContext));

                var connectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    ConnectionString = _mssqlContainer.GetConnectionString(),

                    InitialCatalog = "Api_Test"
                };

                services.AddSqlServer<CatalogDbContext>(connectionStringBuilder.ConnectionString);

            });
    }

    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }
}