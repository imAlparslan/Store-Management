using MassTransit;
using StoreDefinition.Application.Services;

namespace StoreDefinition.Infrastructure.Services;
public class EventPublisher(IPublishEndpoint publisher) :IEventPublisher
{
    private readonly IPublishEndpoint _publisher = publisher;

    public async Task PublishAsync<TEvent>(TEvent @event)
    {
        await _publisher.Publish(@event);
    }
}
