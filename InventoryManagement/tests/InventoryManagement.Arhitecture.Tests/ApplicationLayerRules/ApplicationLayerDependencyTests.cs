namespace InventoryManagement.Arhitecture.Tests.ApplicationLayerRules;

public class ApplicationLayerDependencyTests : ArchUnitBase
{

    [Fact]
    public void ApplicationLayer_ShouldNotDependOnPresentation()
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
    public void ApplicationLayer_ShouldNotDependOnInfrastructure()
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
