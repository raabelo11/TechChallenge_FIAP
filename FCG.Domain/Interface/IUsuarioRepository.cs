using FCG.Domain.Models;

namespace FCG.Domain.Interface
{
    public interface IUsuarioRepository
    {
        Task<bool> Add(Usuario usuario);
        Task<List<Usuario>> List();
        Task<Usuario?> GetByEmail(string email);
    }
}
