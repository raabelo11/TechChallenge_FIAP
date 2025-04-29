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

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = _context.Set<Tentity>().Find(id);
            if (entity != null)
            {
                _context.Set<Tentity>().Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Tentity>> GetAllAsync()
        {
            return await _context.Set<Tentity>().ToListAsync();
        }

        public async Task<Tentity> GetByIdAsync(int id)
        {
            return await _context.Set<Tentity>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(int id,Tentity entity)
        {
            var entityId = _context.Set<Tentity>().Find(id);
            if (entityId != null)
            {
                _context.Set<Tentity>().Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
