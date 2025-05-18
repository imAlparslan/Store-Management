namespace InventoryManagement.Arhitecture.Tests.DomainLayerRules;

public class DomainLayerDependencyTests :ArchUnitBase
{

    [Fact]
    public void DomainLayer_ShouldNotDependOnPresentationLayer()
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
    public void DomainLayer_ShouldNotDependOnApplicationLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(ApplicationLayer)
            .Check(Architecture);

    }

    [Fact]
    public void DomainLayer_ShouldNotDependOnInfratstructureLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(InfrastructureLayer)
            .Check(Architecture);

    }
}

