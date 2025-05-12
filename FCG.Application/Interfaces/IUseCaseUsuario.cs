using FCG.Domain.DTOs;
using FCG.Domain.Models;

namespace FCG.Application.Interfaces
{
    public interface IUseCaseUsuario
    {
        Task<ApiResponse> Add(UsuarioDTO usuarioDTO);
        Task<ApiResponse> List();
        Task<ApiResponse> ListById(Guid id);
        Task<ApiResponse> Update(UsuarioUpdateDTO usuarioUpdateDTO);
        Task<ApiResponse> Delete(Guid id);
    }
}
