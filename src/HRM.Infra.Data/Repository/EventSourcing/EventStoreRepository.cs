using System;
using System.Collections.Generic;
using System.Linq;
using HRM.Domain.Core.Events;
using HRM.Infra.Data.Context;

namespace HRM.Infra.Data.Repository.EventSourcing
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventSourcingContext _context;

        public EventStoreRepository(EventSourcingContext context)
        {
            _context = context;
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return _context.StoredEvent.Where(x=> x.AggregateId == aggregateId).ToList();
        }

        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvent.Add(theEvent);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
