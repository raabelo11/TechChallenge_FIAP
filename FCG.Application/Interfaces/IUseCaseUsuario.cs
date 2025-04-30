using FCG.Domain.DTOs;
using FCG.Domain.Models;

namespace FCG.Application.Interfaces
{
    public interface IUseCaseUsuario
    {
        Task<ApiResponse> Add(UsuarioDTO usuarioDTO);
        Task<ApiResponse> List();
        Task<Usuario?> GetByEmail(string email);
    }
}
