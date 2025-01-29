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
    public HttpClient Client { get; private set; } = default!;

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
                services.RemoveAll<DbContextOptions<CatalogDbContext>>();
                services.RemoveAll<CatalogDbContext>();
                services.RemoveAll<IDomainEventPublisherService>();

                var connectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    ConnectionString = _mssqlContainer.GetConnectionString(),
                    InitialCatalog = "Api_Test"
                };

                services.AddScoped<IDomainEventPublisherService, DomainEventPublisherService>();

                services.AddDbContext<CatalogDbContext>(
                    (sp, opt) => opt
                        .UseSqlServer(connectionString: connectionStringBuilder.ConnectionString)
                        .AddInterceptors(sp.GetRequiredService<DomainEventPublisherInterceptor>())
                    );
            });

    }
    public async Task ResetDb()
    {
        var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }
    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();
        Client = CreateClient();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }
}