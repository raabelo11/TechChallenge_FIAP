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
        [HttpPost("Incluir")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Add([FromBody] UsuarioDTO usuarioDTO)
        {
            var response = await _usuarioUseCase.Add(usuarioDTO);
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("Lista")]
        [Produces(typeof(List<Usuario>))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> List()
        {
            var response = await _usuarioUseCase.List();
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public ActionResult Rascunho2()
        {
            return Ok();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete]
        public ActionResult Rascunho()
        {
            return Ok();
        }
    }
}
