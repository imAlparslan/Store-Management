using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using StoreDefinition.Architecture.Tests.Common;

namespace StoreDefinition.Architecture.Tests.LayerDependencyTests;
public class DomainLayerDependencyTests : ArchUnitBase
{

    [Fact]
    public void DomainLayer_ShouldNotDependOnApplication()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(DomainAssembly)
            .Should()
            .NotDependOnAny(ApplicationLayer)
            .Check(Architecture);
    }

    [Fact]
    public void DomainLayer_ShouldNotDependOnInfrastructure()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(DomainAssembly)
            .Should()
            .NotDependOnAny(InfrastructureLayer)
            .Check(Architecture);
    }

    [Fact]
    public void DomainLayer_ShouldNotDependOnPresentation()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(DomainAssembly)
            .Should()
            .NotDependOnAny(PresentationLayer)
            .Check(Architecture);
    }
}
