using FCG.Domain.Interface;
using FCG.Domain.Models;
using FCG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repository
{
    public class UsuarioRepository(ApplicationDbContext context) : IUsuarioRepository
    {
        private ApplicationDbContext _context = context;

        public async Task<bool> Add(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Usuario>> List()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
