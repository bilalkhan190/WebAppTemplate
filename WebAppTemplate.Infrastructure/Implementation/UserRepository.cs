using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Shared.Enums;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.Infrastructure.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
       

        public async Task<User> FindByIdAsync(int id)
        {
            return await FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await GetAllAsync(x => x.Active == Status.Active);
        }

        public async Task<User> RegisterAsync(User user)
        {
            await AddAsync(user);
            return user;
        }
    }
}
