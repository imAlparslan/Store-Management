using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using CatalogManagement.Architecture.Tests.Commons;

namespace CatalogManagement.Architecture.Tests.LayerDependencyTests;
public class ApplicationLayerDependencyTests : ArchUnitBase
{
    [Fact]
    public void ApplicationLayer_Should_Not_Depend_On_PresentationLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(ApplicationLayer)
            .Should()
            .NotDependOnAny(PresentationLayer)
            .Check(Architecture);
    }

    [Fact]
    public void ApplicationLayer_Should_Not_Depend_On_InfrastructureLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(ApplicationLayer)
            .Should()
            .NotDependOnAny(InfrastructureLayer)
            .Check(Architecture);
    }
}
