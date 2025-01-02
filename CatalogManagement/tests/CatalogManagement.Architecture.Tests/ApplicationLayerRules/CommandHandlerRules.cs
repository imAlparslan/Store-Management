using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Architecture.Tests.Commons;

namespace CatalogManagement.Architecture.Tests.ApplicationLayerRules;
public class CommandHandlerRules : ArchUnitBase
{

    [Fact]
    public void CommandHandlers_Should_Have_Suffix_Of_CommandHandler()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void CommandHandlers_Should_Reside_In_ApplicationLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .ResideInAssembly(ApplicationAssembly)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void CommandHandlers_ShouldBe_Internal()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .BeInternal()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void CommandHandlers_ShouldBe_Sealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .BeSealed()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
}
