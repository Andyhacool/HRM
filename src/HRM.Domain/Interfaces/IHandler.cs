using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Domain.Interfaces
{
    public interface IHandler<T>
        where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
