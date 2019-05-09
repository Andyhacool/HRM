using System;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> GetSingleBySpec(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
        Task SaveChangesAsync();
    }
}
