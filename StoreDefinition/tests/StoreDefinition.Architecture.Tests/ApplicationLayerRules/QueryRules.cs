using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Architecture.Tests.Common;

namespace StoreDefinition.Architecture.Tests.ApplicationLayerRules;
public class QueryRules : ArchUnitBase
{
    [Fact]
    public void Queries_Should_Have_Suffix_Of_Query()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .HaveNameEndingWith(ClassNameConstants.QuerySuffix)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void Queries_Should_Reside_In_ApplicationLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .ResideInAssembly(ApplicationAssembly)
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }

    [Fact]
    public void Queries_ShouldBe_Sealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IQuery<>))
            .Should()
            .BeSealed()
            .WithoutRequiringPositiveResults()
            .Check(Architecture);
    }
}
