using FCG.Application.Interfaces;
using FCG.Domain.DTOs;
using FCG.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController(IUseCaseUsuario usuarioUseCase) : ControllerBase
    {
        private readonly IUseCaseUsuario _usuarioUseCase = usuarioUseCase;

        [AllowAnonymous]
        [HttpPost("CriarUsuario")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Add([FromBody] UsuarioDTO usuarioDTO)
        {
            var response = await _usuarioUseCase.Add(usuarioDTO);
           return response.Ok ? Ok(response) : BadRequest(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("ListarUsuario")]
        [Produces(typeof(List<Usuario>))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> List()
        {
            var response = await _usuarioUseCase.List();
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("ListarUsuario/{id}")]
        [Produces(typeof(List<Usuario>))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> List(Guid id)
        {
            var response = await _usuarioUseCase.ListById(id);
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("AlterarUsuario")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Update(UsuarioUpdateDTO usuarioUpdateDTO)
        {
            var response = await _usuarioUseCase.Update(usuarioUpdateDTO);
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeletarUsuario")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            var response = await _usuarioUseCase.Delete(id);
            return response.Ok ? Ok(response) : BadRequest(response);
        }
    }
}
