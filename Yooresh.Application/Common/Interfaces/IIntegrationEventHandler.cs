using Yooresh.Domain.Events;

namespace Yooresh.Application.Common.Interfaces;

public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent:IntegrationEvent
{
    void Handle(TIntegrationEvent @event);
}