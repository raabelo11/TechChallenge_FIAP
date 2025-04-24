using FCG.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repository
{
    public class ReposityGeneric<Tentity> : IRepositoryGeneric<Tentity> where Tentity : class
    {
        private readonly DbContext _context;
        public ReposityGeneric(DbContext dbContext)
        {
           _context = dbContext;
        }
        public async Task<Tentity> AddAsync(Tentity entity)
        {
            _context.Set<Tentity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<Tentity> DeleteAsync(int id)
        {
            var entity = _context.Set<Tentity>().Find(id);
            if (entity != null)
            {
                _context.Set<Tentity>().Remove(entity);
                _context.SaveChanges();
            }
            return await Task.FromResult(entity);
        }

        public async Task<List<Tentity>> GetAllAsync()
        {
            return await _context.Set<Tentity>().ToListAsync();
        }

        public async Task<Tentity> GetByIdAsync(int id)
        {
            return await _context.Set<Tentity>().FindAsync(id);
        }

        public async Task<Tentity> UpdateAsync(Tentity entity)
        {
            _context.Set<Tentity>().Update(entity);
            await _context.SaveChangesAsync();
            return await Task.FromResult(entity);
        }
    }
}
