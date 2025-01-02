using CatalogManagement.Domain.Common.Interfaces;
using MediatR;

namespace CatalogManagement.Application.Common.Interfaces;
public interface IDomainEventHandler<in TEvent>
    : INotificationHandler<TEvent> where TEvent : IDomainEvent
{ }
