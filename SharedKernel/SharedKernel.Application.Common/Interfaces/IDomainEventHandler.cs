using MediatR;
using SharedKernel.Domain.Common.DomainEventAbstraction;

namespace SharedKernel.Application.Common.Interfaces;
public interface IDomainEventHandler<in TEvent>
    : INotificationHandler<TEvent> where TEvent : IDomainEvent
{ }
