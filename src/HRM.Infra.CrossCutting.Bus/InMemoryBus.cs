using HRM.Domain.Core.Bus;
using HRM.Domain.Core.Commands;
using HRM.Domain.Core.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace HRM.Infra.CrossCutting.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;
        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))//only runs if it's for domain event, not for command event
                _eventStore?.Save(@event);

            return _mediator.Publish(@event);
        }
    }
}
