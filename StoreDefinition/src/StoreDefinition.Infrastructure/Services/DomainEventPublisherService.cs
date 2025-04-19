using MediatR;

namespace StoreDefinition.Infrastructure.Services;
internal class DomainEventPublisherService(IMediator mediator) : IDomainEventPublisherService
{
    private readonly IMediator mediator = mediator;
    private List<IDomainEvent> domainEvents = new();
    public async Task PublishAllAsync(CancellationToken cancellationToken = default)
    {
        var events = domainEvents.ToList();
        domainEvents = new List<IDomainEvent>();

        foreach (var @event in events)
        {
            await mediator.Publish(@event, cancellationToken);
        }
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }
    public void AddDomainEvent(IEnumerable<IDomainEvent> domainEvent)
    {
        domainEvents.AddRange(domainEvent);
    }
}