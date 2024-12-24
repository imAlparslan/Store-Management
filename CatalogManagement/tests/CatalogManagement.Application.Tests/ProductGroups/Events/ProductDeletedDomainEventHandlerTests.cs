using CatalogManagement.Application.ProductGroups.Events;
using CatalogManagement.Application.Tests.TestDoubles;
using CatalogManagement.Domain.ProductAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManagement.Application.Tests.ProductGroups.Events;
public class ProductDeletedDomainEventHandlerTests
{

    [Fact]
    public async Task Handler_Should_Work_When_DataValid()
    {
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var repo = new InMemoryProductGroupRepository();
        var handler = new ProductDeletedDomainEventHandler(repo);
        var group = ProductGroupFactory.CreateRandom();
        group.AddProduct(guid1);
        group.AddProduct(guid2);

        var command = new ProductDeletedDomainEvent(guid1);



        await handler.Handle(command, default);

    }
}
