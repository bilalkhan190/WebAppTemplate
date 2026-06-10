using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Domain.Abstraction
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> RegisterAsync(User user);
    }
}
