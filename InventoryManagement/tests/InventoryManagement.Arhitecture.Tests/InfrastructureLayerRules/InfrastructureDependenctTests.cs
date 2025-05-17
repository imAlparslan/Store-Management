namespace InventoryManagement.Arhitecture.Tests.InfrastructureLayerRules;
public class InfrastructureDependenctTests : ArchUnitBase
{
    [Fact]
    public void InfrastructureLayer_ShouldNotDependOnPresentationLayer()
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

