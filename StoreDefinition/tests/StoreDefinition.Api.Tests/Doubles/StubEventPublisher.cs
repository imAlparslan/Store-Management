using StoreDefinition.Application.Services;

namespace StoreDefinition.Api.Tests.Doubles;
public class StubEventPublisher : IEventPublisher
{
    Task IEventPublisher.PublishAsync<TEvent>(TEvent @event)
    {
        return Task.CompletedTask;
    }
}
