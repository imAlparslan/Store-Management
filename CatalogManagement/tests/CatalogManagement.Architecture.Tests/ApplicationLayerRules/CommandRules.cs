using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using CatalogManagement.Architecture.Tests.Commons;
using CatalogManagement.Architecture.Tests.Constants;

namespace CatalogManagement.Architecture.Tests.ApplicationLayerRules;

public class CommandRules : ArchUnitBase
{
    [Fact]
    public void Commands_Should_Have_Suffix_Of_Command()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(ICommand<>))
            .Should()
            .HaveNameEndingWith(ClassNameConstants.CommandSuffix)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void Commands_Should_Reside_In_ApplicationLayer()
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
    public void Commands_ShouldBe_Sealed()
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
}
