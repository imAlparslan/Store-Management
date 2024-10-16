﻿using CatalogManagement.Application.Common;
using CatalogManagement.Infrastructure.Persistence;
using CatalogManagement.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace CatalogManagement.Infrastructure.Tests.Fixtures;
public class ProductRepositoryFixture : IAsyncLifetime
{
    public IProductRepository _productRepository = null!;
    public IUnitOfWorkManager _unitOfWorkManager = null!;
    private readonly MsSqlContainer _mssqlContainer =
        new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_PID", "Developer")
            .WithName("test_db")
            .WithPassword("test_pWd")
            .Build();
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

        var catalogDbContext = new CatalogDbContext(connectionOption);

        _unitOfWorkManager = new UnitOfWorkManager(catalogDbContext);
        _productRepository = new ProductRepository(catalogDbContext, _unitOfWorkManager);

        await catalogDbContext.Database.EnsureDeletedAsync();
        await catalogDbContext.Database.EnsureCreatedAsync();

    }

    public async Task DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }
}