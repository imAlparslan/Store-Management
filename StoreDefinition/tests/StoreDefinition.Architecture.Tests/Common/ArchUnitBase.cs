using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using StoreDefinition.Api;
using StoreDefinition.Application;
using StoreDefinition.Architecture.Tests.Constants;
using StoreDefinition.Domain;
using StoreDefinition.Infrastructure;
using Assembly = System.Reflection.Assembly;

namespace StoreDefinition.Architecture.Tests.Common;

public abstract class ArchUnitBase
{
    protected static readonly Assembly PresentationAssembly = typeof(IApiAssemblyMarker).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IApplicationAssemblyMarker).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(IInfrastructureAssemblyMarker).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(IDomainAssemblyMarker).Assembly;

    protected static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader()
        .LoadAssemblies(PresentationAssembly, ApplicationAssembly, InfrastructureAssembly, DomainAssembly)
        .Build();

    protected static readonly IObjectProvider<IType> PresentationLayer =
        ArchRuleDefinition.Types()
        .That()
        .ResideInAssembly(PresentationAssembly)
        .As(LayerNames.PresentationLayer);

    protected static readonly IObjectProvider<IType> ApplicationLayer =
        ArchRuleDefinition.Types()
        .That()
        .ResideInAssembly(ApplicationAssembly)
        .As(LayerNames.ApplicationLayer);

    protected static readonly IObjectProvider<IType> InfrastructureLayer =
        ArchRuleDefinition.Types()
        .That()
        .ResideInAssembly(InfrastructureAssembly)
        .As(LayerNames.InfrastructureLayer);

    protected static readonly IObjectProvider<IType> DomainLayer =
        ArchRuleDefinition.Types()
        .That()
        .ResideInAssembly(DomainAssembly)
        .As(LayerNames.DomainLayer);
}

