using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using StoreDefinition.Infrastructure.Persistence;
using StoreDefinition.Infrastructure.Persistence.Interceptors;
using StoreDefinition.Infrastructure.Services;
using Testcontainers.MsSql;

namespace StoreDefinition.Api.Tests;
public class StoreDefinitionApiFactory : WebApplicationFactory<IApiAssemblyMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer msSqlContainer;
    public HttpClient client { get; private set; } = default!;

    public StoreDefinitionApiFactory()
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

        builder.ConfigureLogging(cfg => cfg.ClearProviders());

        builder.ConfigureTestServices(
            services =>
            {
                services.RemoveAll<DbContextOptions<StoreDefinitionDbContext>>();
                services.RemoveAll<StoreDefinitionDbContext>();
                services.RemoveAll<IDomainEventPublisherService>();

                var connectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    ConnectionString = msSqlContainer.GetConnectionString(),
                    InitialCatalog = "Api_Test"
                };

                services.AddScoped<IDomainEventPublisherService, DomainEventPublisherService>();

                services.AddDbContext<StoreDefinitionDbContext>(
                    (sp, opt) => opt
                        .UseSqlServer(connectionString: connectionStringBuilder.ConnectionString)
                        .AddInterceptors(sp.GetRequiredService<DomainEventPublisherInterceptor>())
                    );
            });
    }

    public async Task ResetDb()
    {
        var scope = this.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<StoreDefinitionDbContext>();
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
