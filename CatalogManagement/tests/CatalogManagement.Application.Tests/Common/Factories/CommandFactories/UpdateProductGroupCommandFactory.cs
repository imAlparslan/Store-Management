using Bogus;
using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Application.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManagement.Application.Tests.Common.Factories.CommandFactories;
internal class UpdateProductGroupCommandFactory
{
    public static UpdateProductGroupCommand CreateCustom(string productGroupName, string productGroupDescription)
    {
        return new Faker<UpdateProductGroupCommand>()
            .CustomInstantiator(x => new(Guid.NewGuid(), productGroupName, productGroupDescription))
            .Generate();
    }

    public static UpdateProductGroupCommand CreateCustom(Guid id, string productGroupName, string productGroupDescription)
    {
        return new Faker<UpdateProductGroupCommand>()
            .CustomInstantiator(x => new(id, productGroupName, productGroupDescription))
            .Generate();
    }

    public static UpdateProductGroupCommand CreateValid()
    {
        return new Faker<UpdateProductGroupCommand>()
             .CustomInstantiator(x => new(Guid.NewGuid(), x.Commerce.ProductName(), x.Commerce.ProductDescription()))
             .Generate();
    }
    public static UpdateProductGroupCommand CreateWithId(Guid id)
    {
        return new Faker<UpdateProductGroupCommand>()
             .CustomInstantiator(x => new(id, x.Commerce.ProductName(), x.Commerce.ProductDescription()))
             .Generate();
    }

    public static UpdateProductGroupCommand CreateWithName(string productName)
    {
        return new Faker<UpdateProductGroupCommand>()
            .CustomInstantiator(x => new(Guid.NewGuid(), productName, x.Commerce.ProductDescription()))
            .Generate();
    }
  
    public static UpdateProductGroupCommand CreateWithDefinition(string productGroupDescription)
    {
        return new Faker<UpdateProductGroupCommand>()
            .CustomInstantiator(x => new(Guid.NewGuid(), x.Commerce.ProductName(), productGroupDescription))
            .Generate();
    }
}
