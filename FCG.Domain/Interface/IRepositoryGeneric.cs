using FCG.Domain.Models;

namespace FCG.Domain.Interface
{
    public interface IRepositoryGeneric<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
