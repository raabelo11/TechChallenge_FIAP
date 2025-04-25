using FCG.Application.Interfaces;
using FCG.Domain.DTOs;
using FCG.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController(IUseCaseUsuario usuarioUseCase) : Controller
    {
        private readonly IUseCaseUsuario _usuarioUseCase = usuarioUseCase;

        [HttpPost("Incluir")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Add([FromBody] UsuarioDTO usuarioDTO)
        {
            var response = await _usuarioUseCase.Add(usuarioDTO);
            return response.Ok ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Lista")]
        [Produces(typeof(List<Usuario>))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> List()
        {
            var response = await _usuarioUseCase.List();
            return response.Ok ? Ok(response) : BadRequest(response);
        }
    }
}
