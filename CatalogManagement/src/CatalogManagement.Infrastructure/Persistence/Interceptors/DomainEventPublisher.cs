using CatalogManagement.Domain.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CatalogManagement.Infrastructure.Persistence.Interceptors;
public class DomainEventPublisher : SaveChangesInterceptor
{
    private readonly IMediator mediator;
    private IReadOnlyList<IDomainEvent> domainEvents;
    public DomainEventPublisher(IMediator mediator)
    {
        this.mediator = mediator;
        domainEvents = new List<IDomainEvent>();
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        FilterDomainEvents(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        FilterDomainEvents(eventData);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {

        await PublishDomainEvents(cancellationToken);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        PublishDomainEvents().GetAwaiter().GetResult();

        return base.SavedChanges(eventData, result);
    }

    private async Task PublishDomainEvents(CancellationToken cancellationToken = default)
    {
        var events = domainEvents;
        domainEvents = new List<IDomainEvent>();
        foreach (var domainEvent in events)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }
    }

    private void FilterDomainEvents(DbContextEventData eventData)
    {
        if (eventData.Context is not null)
        {
            domainEvents = eventData.Context.ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity)
                    .SelectMany(x =>
                    {
                        List<IDomainEvent> events = x.GetDomainEvents().ToList();
                        x.ClearDomainEvents();
                        return events;
                    })
                    .ToList();
        }
    }
}
