using System.Linq;
using HRM.Domain;
using HRM.Domain.Interfaces;
using HRM.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infra.Data.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HRMContext context)
            : base(context)
        {

        }

        public Employee GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}
