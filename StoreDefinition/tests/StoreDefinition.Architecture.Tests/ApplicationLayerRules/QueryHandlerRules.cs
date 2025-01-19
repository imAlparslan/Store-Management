using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Architecture.Tests.Common;

namespace StoreDefinition.Architecture.Tests.ApplicationLayerRules;
public class QueryHandlerRules : ArchUnitBase
{

    [Fact]
    public void QueryHandlers_Should_Have_Suffix_Of_QueryHandler()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith(ClassNameConstants.QueryHandlerSuffix)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void QueryHandlers_Should_Reside_In_ApplicationLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .ResideInAssembly(ApplicationAssembly)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void QueryHandlers_ShouldBe_Internal()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeInternal()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void QueryHandlers_ShouldBe_Sealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeSealed()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
}
