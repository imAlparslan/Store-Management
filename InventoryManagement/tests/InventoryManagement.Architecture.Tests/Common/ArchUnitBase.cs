using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using InventoryManagement.Api;
using InventoryManagement.Application;
using InventoryManagement.Arhitecture.Tests.Constants;
using InventoryManagement.Domain;
using InventoryManagement.Infrastructure;
using Assembly = System.Reflection.Assembly;

namespace InventoryManagement.Arhitecture.Tests.Common;
public abstract class ArchUnitBase
{
    protected static readonly Assembly PresentationAssembly = typeof(IApiAssemblyMarker).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IApplicationAssemblyMarker).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(IDomaninAssemblyMarker).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(IInfrastructureAssemblyMarker).Assembly;

    protected static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(PresentationAssembly, ApplicationAssembly, DomainAssembly, InfrastructureAssembly)
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

    protected static readonly IObjectProvider<IType> DomainLayer =
        ArchRuleDefinition.Types()
        .That()
        .ResideInAssembly(DomainAssembly)
        .As(LayerNames.DomainLayer);
    
    protected static readonly IObjectProvider<IType> InfrastructureLayer =
        ArchRuleDefinition.Types()
        .That()
        .ResideInAssembly(InfrastructureAssembly)
        .As(LayerNames.InfrastructureLayer);

}

