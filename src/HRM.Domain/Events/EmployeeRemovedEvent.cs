using HRM.Domain.Core.Events;
using System;

namespace HRM.Domain.Events
{
    public class EmployeeRemovedEvent : Event
    {
        public EmployeeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}