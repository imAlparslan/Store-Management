using CatalogManagement.Domain.Common.Interfaces;
using MediatR;

namespace CatalogManagement.Application.Common.Interfaces;
public interface IDomainEventHandler<TEvent>
    : INotificationHandler<TEvent> where TEvent : IDomainEvent
{ }
