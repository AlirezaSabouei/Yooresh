using Yooresh.Domain.Events;

namespace Yooresh.Application.Common.Interfaces;

public interface IEventBus
{
    void Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent;

    void Subscribe<TEvent, TEventHandler>(TEventHandler eventHandler) 
        where TEvent : IntegrationEvent
        where TEventHandler : IIntegrationEventHandler<IntegrationEvent>;

    void StartConsuming();
}