using System;
using System.Threading.Tasks;

namespace HRM.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
