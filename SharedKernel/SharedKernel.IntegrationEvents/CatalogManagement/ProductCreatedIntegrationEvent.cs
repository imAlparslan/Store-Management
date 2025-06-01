using SharedKernel.IntegrationEvents.Abstract;

namespace SharedKernel.IntegrationEvents.CatalogManagement;
public class ProductCreatedIntegrationEvent : IIntegrationEvent
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Definition { get; set; } = default!;
    public bool IsDefault { get; set; } = false;
}
