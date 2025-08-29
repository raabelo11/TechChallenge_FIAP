using FCG.Application.Interfaces;
using FCG.Domain.DTOs;
using FCG.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Controllers
{
    /// <summary>
    ///     API Usuarios
    /// </summary>
    /// <param name="usuarioUseCase"></param>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController(IUseCaseUsuario usuarioUseCase) : ControllerBase
    {
        private readonly IUseCaseUsuario _usuarioUseCase = usuarioUseCase;

        /// <summary>
        ///     Endpoint responsável por criar um usuário dos tipos Usuario = 1, Administrador = 2
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CriarUsuario")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Add([FromBody] UsuarioDTO usuarioDTO)
        {
            var response = await _usuarioUseCase.Add(usuarioDTO);
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        ///     Endpoint responsável por listar todos os usuários do sistema
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("ListarUsuario")]
        [Produces(typeof(List<Usuario>))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> List()
        {
            var response = await _usuarioUseCase.List();
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        ///     Endpoint responsável por listar um usuário por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("ListarUsuario/{id}")]
        [Produces(typeof(Usuario))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> List(Guid id)
        {
            var response = await _usuarioUseCase.ListById(id);
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        ///     Endpoint responsável por alterar um usuário
        /// </summary>
        /// <param name="usuarioUpdateDTO"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("AlterarUsuario")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Update(UsuarioUpdateDTO usuarioUpdateDTO)
        {
            var response = await _usuarioUseCase.Update(usuarioUpdateDTO);
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        ///     Endpoint responsável por excluir um usuário pelo seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeletarUsuario")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            var response = await _usuarioUseCase.Delete(id);
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [Route("Teste")]
        public ActionResult Teste(Guid id)
        {
            return Ok("Acesso Liberado!");
        }
    }
}
