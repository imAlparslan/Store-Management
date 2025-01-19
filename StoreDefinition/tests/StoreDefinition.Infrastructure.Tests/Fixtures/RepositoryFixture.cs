namespace StoreDefinition.Infrastructure.Tests.Fixtures;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Infrastructure.Persistence;
using StoreDefinition.Infrastructure.Repositories;
using System.Threading.Tasks;
using Testcontainers.MsSql;

public class RepositoryFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _mssqlContainer;
    private StoreDefinitionDbContext _storeDefinitionDbContext = null!;
    private IUnitOfWorkManager unitOfWorkManager = null!;

    public IShopRepository ShopRepository { get; private set; } = null!;
    public IGroupRepository GroupRepository { get; private set; } = null!;
    public RepositoryFixture()
    {
        _mssqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_PID", "Developer")
            .WithPassword("test_pWd")
            .Build();
    }

    public async Task DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();

        var connectionStringBuilder = new SqlConnectionStringBuilder()
        {
            ConnectionString = _mssqlContainer.GetConnectionString(),
            InitialCatalog = "TestContainerDb"
        };

        var connectionOption = new DbContextOptionsBuilder<StoreDefinitionDbContext>()
            .UseSqlServer(connectionStringBuilder.ConnectionString)
            .Options;

        _storeDefinitionDbContext = new StoreDefinitionDbContext(connectionOption);
        unitOfWorkManager = new UnitOfWorkManager(_storeDefinitionDbContext);
        ShopRepository = new ShopRepository(_storeDefinitionDbContext, unitOfWorkManager);
        GroupRepository = new GroupRepository(_storeDefinitionDbContext, unitOfWorkManager);

    }

    public void ResetDb()
    {
        _storeDefinitionDbContext.Database.EnsureDeleted();
        _storeDefinitionDbContext.Database.EnsureCreated();
    }
}
