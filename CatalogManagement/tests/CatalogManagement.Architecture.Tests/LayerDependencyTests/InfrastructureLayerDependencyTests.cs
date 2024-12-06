using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using CatalogManagement.Architecture.Tests.Commons;

namespace CatalogManagement.Architecture.Tests.LayerDependencyTests;
public class InfrastructureLayerDependencyTests : ArchUnitBase
{

    [Fact]
    public void Infrastructure_Should_Not_Depend_PresentationLayer()
    {

        ArchRuleDefinition
            .Types()
            .That()
            .Are(InfrastructureLayer)
            .Should()
            .NotDependOnAny(PresentationLayer)
            .Check(Architecture);
    }
}
