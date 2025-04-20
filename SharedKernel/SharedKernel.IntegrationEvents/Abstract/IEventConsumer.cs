using MassTransit;
namespace SharedKernel.IntegrationEvents.Abstract
{
    public interface IEventConsumer<in TEvent> : IConsumer<TEvent>
        where TEvent : class, IIntegrationEvent
    {

    }
}
