using HRM.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Infra.CrossCutting.Identity.Events
{
    public class UserRegisteredEvent : Event
    {
        public UserRegisteredEvent(Guid id, string userName, string email)
        {
            Id = id;
            UserName = userName;
            Email = email;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string UserName { get; private set; }

        public string Email { get; private set; }
    }
}
