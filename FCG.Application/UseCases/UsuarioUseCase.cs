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
                    Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha),
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
                    Ok = false,
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
                    Ok = false,
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }

        public async Task<ApiResponse> ListById(Guid id)
        {
            try
            {
                return new ApiResponse
                {
                    Ok = true,
                    Data = await _usuarioRepository.GetByIdAsync(id)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Ok = false,
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }

        public async Task<ApiResponse> Update(UsuarioUpdateDTO usuarioUpdateDTO)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(usuarioUpdateDTO.Id);

                usuario = new Usuario()
                {
                    Nome = usuarioUpdateDTO.Nome,
                    Email = usuarioUpdateDTO.Email,
                    Senha = usuarioUpdateDTO.Senha,
                    Tipo = usuarioUpdateDTO.Tipo
                };

                if (usuario != null)
                {
                    var update = await _usuarioRepository.UpdateAsync(usuario);
                }
                else
                {
                    bool sucesso = await _usuarioRepository.AddAsync(usuario);
                }

                return new ApiResponse
                {
                    Ok = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Ok = false,
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }

        public async Task<ApiResponse> Delete (Guid id)
        {
            if (await _usuarioRepository.GetByIdAsync(id) != null)
            {
                bool sucesso = await _usuarioRepository.DeleteAsync(id);
            }

            return new ApiResponse
            {
                Ok = true,
            };
        }
    }
}
