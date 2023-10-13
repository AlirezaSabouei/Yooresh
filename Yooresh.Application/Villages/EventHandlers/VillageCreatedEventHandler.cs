using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Events;

namespace Yooresh.Application.Villages.EventHandlers;

public class VillageCreatedEventHandler:IIntegrationEventHandler<VillageCreatedEvent>
{
    public void Handle(VillageCreatedEvent @event)
    {
       // throw new NotImplementedException();
    }
}