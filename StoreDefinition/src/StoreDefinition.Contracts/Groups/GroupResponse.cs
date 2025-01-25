namespace StoreDefinition.Contracts.Groups;
public sealed record GroupResponse(Guid Id,
                            string Name,
                            string Description,
                            IReadOnlyList<Guid> ShopsIds);
