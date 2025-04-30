using FCG.Application.Interfaces;
using FCG.Domain.Interface;
using FCG.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FCG.Application.Authorization
{
    public class AuthService(IConfiguration configuration, IUsuarioRepository usuarioRepository) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<ApiResponse> Login(LoginModel login)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByEmail(login.Email);
                if (usuario == null)
                {
                    return new ApiResponse
                    {
                        Ok = false,
                        Errors = ["Usuário ou senha inválidos."]
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha))
                {
                    return new ApiResponse
                    {
                        Ok = false,
                        Errors = ["Usuário ou senha inválidos."]
                    };
                }

                var token = this.GerarToken(usuario);
                if (token.IsNullOrEmpty())
                {
                    return new ApiResponse
                    {
                        Ok = false,
                        Errors = ["Erro na geração do Token"]
                    };
                }

                return new ApiResponse
                {
                    Ok = true,
                    Data = token
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

        public string GerarToken(Usuario usuario)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
