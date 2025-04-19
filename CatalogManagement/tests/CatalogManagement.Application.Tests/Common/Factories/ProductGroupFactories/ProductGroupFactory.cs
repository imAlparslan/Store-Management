using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.Tests.Common.Factories.ProductGroupFactories;
internal class ProductGroupFactory
{
    public static ProductGroup CreateDefault()
    {
        return new ProductGroup(new ProductGroupName("product group name"),
            new ProductGroupDescription("product group descripton"));
    }

    public static ProductGroup CreateRandom()
    {
        var name = ProductGroupNameFactory.CreateRandom();
        var description = ProductGroupDescriptionFactory.CreateRandom();

        return new ProductGroup(name, description, Guid.NewGuid());
    }

    public static ProductGroup CreateFromCreateCommand(CreateProductGroupCommand command)
    {
        return new ProductGroup(new(command.Name), new(command.Description));
    }

    public static ProductGroup CreateFromCreateCommand(UpdateProductGroupCommand command)
    {
        return new ProductGroup(new(command.Name), new(command.Description), command.Id);
    }
}
