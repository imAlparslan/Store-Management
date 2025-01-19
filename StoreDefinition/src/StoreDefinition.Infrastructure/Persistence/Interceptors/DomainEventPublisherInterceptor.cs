using Microsoft.EntityFrameworkCore.Diagnostics;
using StoreDefinition.Domain.Common.Interfaces;
using StoreDefinition.Infrastructure.Services;

namespace StoreDefinition.Infrastructure.Persistence.Interceptors;
internal class DomainEventPublisherInterceptor : SaveChangesInterceptor
{

    private readonly IDomainEventPublisherService _publisher;
    public DomainEventPublisherInterceptor(IDomainEventPublisherService publisher)
    {
        _publisher = publisher;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        _publisher.AddDomainEvent(GetDomainEvents(eventData));
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        _publisher.AddDomainEvent(GetDomainEvents(eventData));
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
        await _publisher.PublishAllAsync(cancellationToken);
    }

    private static List<IDomainEvent> GetDomainEvents(DbContextEventData eventData)
    {
        if (eventData.Context is not null)
        {
            return eventData.Context.ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity)
                .SelectMany(x =>
                {
                    List<IDomainEvent> events = x.GetDomainEvents().ToList();
                    x.ClearDomainEvents();
                    return events;
                })
                .ToList();
        }
        return new List<IDomainEvent>();
    }
}
