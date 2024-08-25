using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using CatalogManagement.Architecture.Tests.Commons;

namespace CatalogManagement.Architecture.Tests.LayerDependencyTests;
public class DomainLayerDependencyTests : ArchUnitBase
{
    [Fact]
    public void DomainLayer_Should_Not_Depend_PresentationLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(PresentationLayer)
            .Check(Architecture);
    }

    [Fact]
    public void DomainLayer_Should_Not_Depend_InfrastructureLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(InfrastructureLayer)
            .Check(Architecture);
    }

    [Fact]
    public void DomainLayer_Should_Not_Depend_ApplicationLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(ApplicationLayer)
            .Check(Architecture);
    }

}
