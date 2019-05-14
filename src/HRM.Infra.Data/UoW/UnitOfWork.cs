using HRM.Domain.Interfaces;
using HRM.Infra.Data.Context;

namespace HRM.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRMContext _context;

        public UnitOfWork(HRMContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
