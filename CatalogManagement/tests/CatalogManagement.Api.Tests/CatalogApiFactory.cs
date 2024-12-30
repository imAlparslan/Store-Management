using CatalogManagement.Infrastructure.Persistence.Interceptors;
using CatalogManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.MsSql;

namespace CatalogManagement.Api.Tests;

public class CatalogApiFactory
    : WebApplicationFactory<IApiAssemblyMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer _mssqlContainer = null!;

    public CatalogApiFactory()
    {
        _mssqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_PID", "Developer")
            .WithPassword("test_pWd")
            .Build();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging => logging.ClearProviders());

        builder.ConfigureTestServices(
            services =>
            {
                services.RemoveAll(typeof(DbContextOptions<CatalogDbContext>));
                services.RemoveAll(typeof(CatalogDbContext));
                services.RemoveAll(typeof(IDomainEventPublisherService));

                var connectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    ConnectionString = _mssqlContainer.GetConnectionString(),
                    InitialCatalog = "Api_Test"
                };

                services.AddScoped<IDomainEventPublisherService, DomainEventPublisherService>();

                services.AddDbContext<CatalogDbContext>(
                    (sp, opt) => opt
                        .UseSqlServer(connectionString: connectionStringBuilder.ConnectionString)
                        .AddInterceptors(sp.GetRequiredService<DomainEventPublisher>())
                    );
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