using FCG.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repository
{
    public class ReposityGeneric<Tentity>(DbContext dbContext) : IRepositoryGeneric<Tentity> where Tentity : class
    {
        private readonly DbContext _context = dbContext;

        public async Task<List<Tentity>> GetAllAsync()
        {
            return await _context.Set<Tentity>().AsNoTracking().ToListAsync();
        }
        public async Task<bool> AddAsync(Tentity entity)
        {
            _context.Set<Tentity>().Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = _context.Set<Tentity>().Find(id);
            if (entity != null)
            {
                _context.Set<Tentity>().Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Tentity> GetByEmail(string email)
        {
            return await _context.Set<Tentity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => EF.Property<string>(x, "Email") == email);
        }

        public async Task<Tentity> GetByIdAsync(Guid id)
        {
            return await _context.Set<Tentity>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Tentity entity)
        {
            _context.Set<Tentity>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
