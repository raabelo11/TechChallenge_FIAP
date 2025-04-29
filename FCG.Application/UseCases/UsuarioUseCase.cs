using FCG.Application.Interfaces;
using FCG.Domain.DTOs;
using FCG.Domain.Interface;
using FCG.Domain.Models;

namespace FCG.Application.UseCases
{
    public class UsuarioUseCase(IUsuarioRepository usuarioRepository) : IUseCaseUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<ApiResponse> Add(UsuarioDTO usuarioDTO)
        {
            try
            {              
                var usuario = new Usuario
                {
                    Nome = usuarioDTO.Nome,
                    Email = usuarioDTO.Email,
                    Senha = usuarioDTO.Senha,
                    Tipo = usuarioDTO.Tipo
                };

                return new ApiResponse
                {
                    Ok = await _usuarioRepository.AddAsync(usuario),
                };
                
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }

        public async Task<ApiResponse> List()
        {
            try
            {
                return new ApiResponse
                {
                    Ok = true,
                    Data = await _usuarioRepository.GetAllAsync()
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }
    }
}
