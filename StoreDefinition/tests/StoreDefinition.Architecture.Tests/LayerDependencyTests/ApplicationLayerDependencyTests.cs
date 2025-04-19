using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using StoreDefinition.Architecture.Tests.Common;

namespace StoreDefinition.Architecture.Tests.LayerDependencyTests;
public class ApplicationLayerDependencyTests : ArchUnitBase
{

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependenciesOnInfrastructure()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(ApplicationAssembly)
            .Should()
            .NotDependOnAny(InfrastructureLayer)
            .Check(Architecture);
    }
    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependenciesOnPresentation()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(ApplicationAssembly)
            .Should()
            .NotDependOnAny(PresentationLayer)
            .Check(Architecture);
    }
}
