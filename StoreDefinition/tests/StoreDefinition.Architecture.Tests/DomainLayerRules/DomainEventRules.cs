using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using StoreDefinition.Architecture.Tests.Common;

namespace StoreDefinition.Architecture.Tests.DomainLayerRules;
public class DomainEventRules : ArchUnitBase
{
    [Fact]
    public void DomainEvents_Should_Reside_In_DomainLayer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .ResideInAssembly(DomainAssembly)
            .Check(Architecture);
    }
    [Fact]
    public void DomainEvents_Should_Have_Suffix_Of_DomainEvent()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith(ClassNameConstants.DomainEventSuffix)
            .Check(Architecture);
    }

    [Fact]
    public void DomainEvents_Should_Be_Sealed()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .Check(Architecture);
    }

    [Fact]
    public void DomainEvents_Should_Be_Record()
    {
        ArchRuleDefinition
            .Classes()
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeRecord()
            .Check(Architecture);
    }
}
