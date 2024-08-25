using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using CatalogManagement.Architecture.Tests.Constants;
using Assembly = System.Reflection.Assembly;

namespace CatalogManagement.Architecture.Tests.Commons;
public class ArchUnitBase
{
    protected static readonly Assembly PresentationAssembly = typeof(Api.AssemblyInfo).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.AssemblyInfo).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(Application.AssemblyInfo).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(Domain.AssemblyInfo).Assembly;


    protected static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            PresentationAssembly,
            InfrastructureAssembly,
            ApplicationAssembly,
            DomainAssembly)
        .Build();

    protected static readonly IObjectProvider<IType> PresentationLayer =
        ArchRuleDefinition.Types().That()
        .ResideInAssembly(PresentationAssembly)
        .As(LayerNames.PresentationLayer);

    protected static readonly IObjectProvider<IType> InfrastructureLayer =
        ArchRuleDefinition.Types().That()
        .ResideInAssembly(InfrastructureAssembly)
        .As(LayerNames.InfrastructureLayer);

    protected static readonly IObjectProvider<IType> ApplicationLayer =
        ArchRuleDefinition.Types().That()
        .ResideInAssembly(ApplicationAssembly)
        .As(LayerNames.ApplicationLayer);

    protected static readonly IObjectProvider<IType> DomainLayer =
        ArchRuleDefinition.Types().That()
        .ResideInAssembly(DomainAssembly)
        .As(LayerNames.DomainLayer);
}

