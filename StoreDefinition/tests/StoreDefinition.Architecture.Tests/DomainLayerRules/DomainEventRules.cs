﻿using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using StoreDefinition.Architecture.Tests.Common;
using StoreDefinition.Domain.Common.Interfaces;

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
            .HaveNameEndingWith(ClassNameSuffixes.DomainEventSuffix)
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
