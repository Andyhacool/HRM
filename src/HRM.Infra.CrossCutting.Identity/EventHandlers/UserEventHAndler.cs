using HRM.Infra.CrossCutting.Identity.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRM.Infra.CrossCutting.Identity.EventHandlers
{
    public class UserEventHandler :
        INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }
    }
}
