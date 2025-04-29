using FCG.Domain.Interface;
using FCG.Domain.Models;
using FCG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repository
{
    public class UsuarioRepository : ReposityGeneric<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
