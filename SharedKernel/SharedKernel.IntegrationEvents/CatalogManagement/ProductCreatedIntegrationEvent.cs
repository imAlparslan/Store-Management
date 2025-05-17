using SharedKernel.IntegrationEvents.Abstract;

namespace SharedKernel.IntegrationEvents.CatalogManagement;
public class ProductCreatedIntegrationEvent : IIntegrationEvent
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Definition { get; set; }
    public bool IsDefault { get; set; } = false;
}
