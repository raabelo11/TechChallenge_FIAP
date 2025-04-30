using FCG.Domain.Models;

namespace FCG.Application.Interfaces
{
    public interface IAuthService
    {
        string GerarToken(Usuario usuario);
        Task<ApiResponse> Login(LoginModel login);
    }
}