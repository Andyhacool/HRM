using HRM.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRM.Domain.EventHandlers
{

    public class EmployeeEventHandler :
        INotificationHandler<EmployeeRegisteredEvent>,
        INotificationHandler<EmployeeUpdatedEvent>,
        INotificationHandler<EmployeeRemovedEvent>
    {
        public Task Handle(EmployeeUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(EmployeeRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(EmployeeRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
