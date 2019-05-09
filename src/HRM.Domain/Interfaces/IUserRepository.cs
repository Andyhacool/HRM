using HRM.Domain.Models;
using HRM.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByName(string userName);
        Task<bool> CheckPassword(User user, string password);
    }
}
