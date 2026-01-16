namespace InventoryManagement.Architecture.Tests.ApplicationLayerRules;
public class CommandRules : ArchUnitBase
{
    [Fact]
    public void Commands_ShouldBe_Resides_In_ApplicationLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .ResideInAssembly(ApplicationAssembly)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
    [Fact]
    public void Commands_Should_Be_Sealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .BeSealed()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
    [Fact]
    public void Commands_Should_Have_Suffix_Of_Command()
    {
        ArchRuleDefinition.Types()
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .HaveNameEndingWith(ClassNameConstants.CommandSuffix)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
}