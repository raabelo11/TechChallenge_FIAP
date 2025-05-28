using FCG.Application.Interfaces;
using FCG.Domain.DTOs;
using FCG.Domain.Interface;
using FCG.Domain.Models;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases
{
    public class UsuarioUseCase(IUsuarioRepository usuarioRepository, ILogger<UsuarioUseCase> logger) : IUseCaseUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly ILogger<UsuarioUseCase> _logger = logger;

        public async Task<ApiResponse> Add(UsuarioDTO usuarioDTO)
        {
            try
            {
                _logger.LogInformation($" ======> Usuário a ser enviado para inclusão: Nome: {usuarioDTO.Nome}, Email: {usuarioDTO.Email}, Tipo: {usuarioDTO.Tipo} <======");

                var checkEmail = await _usuarioRepository.GetByEmail(usuarioDTO.Email);

                if (checkEmail != null)
                {
                    return new ApiResponse
                    {
                        Ok = true,
                        Data = "E-mail já existente."
                    };
                }

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
                _logger.LogError($"Exception: {ex.Message}, {ex.StackTrace}");
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
                _logger.LogError($"Exception: {ex.Message}, {ex.StackTrace}");
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
                _logger.LogError($"Exception: {ex.Message}, {ex.StackTrace}");
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
                _logger.LogInformation($" ======> Usuário a ser atualizado: Guid: {usuarioUpdateDTO.Id}, Nome: {usuarioUpdateDTO.Nome}, Email: {usuarioUpdateDTO.Email} <======");

                var usuario = await _usuarioRepository.GetByIdAsync(usuarioUpdateDTO.Id);

                if (usuario != null)
                {
                    usuario.Nome = usuarioUpdateDTO.Nome;
                    usuario.Email = usuarioUpdateDTO.Email;
                    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioUpdateDTO.Senha);
                    usuario.Tipo = usuarioUpdateDTO.Tipo;

                    var update = await _usuarioRepository.UpdateAsync(usuario);
                }
                else
                {
                    return new ApiResponse
                    {
                        Ok = true,
                        Data = "Nenhuma alteração foi realizada."
                    };
                }

                if (usuario.Email != usuarioUpdateDTO.Email)
                {
                    var checkEmail = await _usuarioRepository.GetByEmail(usuarioUpdateDTO.Email);

                    if (checkEmail != null)
                    {
                        return new ApiResponse
                        {
                            Ok = true,
                            Data = "E-mail já existente."
                        };
                    }
                }

                return new ApiResponse
                {
                    Ok = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}, {ex.StackTrace}");
                return new ApiResponse
                {
                    Ok = false,
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }

        public async Task<ApiResponse> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation($"Usuario a ser excluido: ID: {id}");

                if (await _usuarioRepository.GetByIdAsync(id) != null)
                {
                    bool sucesso = await _usuarioRepository.DeleteAsync(id);
                }
                else
                {
                    return new ApiResponse
                    {
                        Ok = true,
                        Data = "Nenhuma alteração foi realizada."
                    };
                }

                return new ApiResponse
                {
                    Ok = true,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}, {ex.StackTrace}");
                return new ApiResponse
                {
                    Ok = false,
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }
    }
}
