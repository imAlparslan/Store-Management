using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Infrastructure.Persistence;
using CatalogManagement.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace CatalogManagement.Infrastructure.Tests.Fixtures;
public class ProductGroupRepositoryFixture : IAsyncLifetime
{
    public IProductGroupRepository _productGroupRepository = null!;
    public IUnitOfWorkManager _unitOfWorkManager = null!;
    private readonly MsSqlContainer _mssqlContainer = null!;

    private CatalogDbContext _catalogDbContext = null!;

    public ProductGroupRepositoryFixture()
    {
        _mssqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_PID", "Developer")
            .WithPassword("test_pWd")
            .Build();
    }
    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();
        var connectionStringBuilder = new SqlConnectionStringBuilder()
        {
            ConnectionString = _mssqlContainer.GetConnectionString(),
            InitialCatalog = "TestContainerDb"
        };

        var connectionOption = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseSqlServer(connectionStringBuilder.ConnectionString)
            .Options;

        _catalogDbContext = new CatalogDbContext(connectionOption);

        _unitOfWorkManager = new UnitOfWorkManager(_catalogDbContext);
        _productGroupRepository = new ProductGroupRepository(_catalogDbContext, _unitOfWorkManager);

    }
    public async Task DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }
    public void RecreateDb()
    {
        _catalogDbContext.Database.EnsureDeleted();
        _catalogDbContext.Database.EnsureCreated();
    }
}
