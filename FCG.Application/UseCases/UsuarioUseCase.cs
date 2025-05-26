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

        public async Task<ApiResponse> Delete(Guid id)
        {
            try
            {
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
                return new ApiResponse
                {
                    Ok = false,
                    Errors = [$"{ex.Message}, {ex.StackTrace}"]
                };
            }
        }
    }
}
