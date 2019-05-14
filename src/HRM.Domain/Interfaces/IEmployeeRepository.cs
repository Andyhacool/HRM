
namespace HRM.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee GetByEmail(string email);
    }
}