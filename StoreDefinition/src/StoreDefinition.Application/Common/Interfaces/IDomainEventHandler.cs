using MediatR;
using StoreDefinition.Domain.Common.Interfaces;

namespace StoreDefinition.Application.Common.Interfaces;
public interface IDomainEventHandler<in TEvent>
    : INotificationHandler<TEvent> where TEvent : IDomainEvent
{ }
