using SharedKernel.IntegrationEvents.Abstract;

namespace StoreDefinition.Application.Services;
public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IIntegrationEvent;
}

