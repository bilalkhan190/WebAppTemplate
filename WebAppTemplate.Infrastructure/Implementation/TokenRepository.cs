using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.Infrastructure.Implementation
{
    public class TokenRepository : Repository<RefreshToken>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task RevokeAsync(string token)
        {
           RefreshToken tokenObject = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
            tokenObject.RevokedAt = DateTime.UtcNow;
            
        }
    }
}
