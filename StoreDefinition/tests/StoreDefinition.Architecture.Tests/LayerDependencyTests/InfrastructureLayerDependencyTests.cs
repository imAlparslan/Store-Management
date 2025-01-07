using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using StoreDefinition.Architecture.Tests.Common;

namespace StoreDefinition.Architecture.Tests.LayerDependencyTests;
public class InfrastructureLayerDependencyTests : ArchUnitBase
{
    [Fact]
    public void InfrastructureLayer_ShouldNotDependOnPresentation()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ResideInAssembly(InfrastructureAssembly)
            .Should()
            .NotDependOnAny(PresentationLayer)
            .Check(Architecture);
    }
}
