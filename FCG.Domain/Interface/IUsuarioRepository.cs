using FCG.Domain.Models;

namespace FCG.Domain.Interface
{
    public interface IUsuarioRepository : IRepositoryGeneric<Usuario>
    {
        Task<Usuario?> GetByEmail(string email);
    }
}
