using System.Threading.Tasks;
using FCG.Application.Interfaces;
using FCG.Domain.DTOs;
using FCG.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController(IUseCaseJogo useCaseJogo) : Controller
    {
        private readonly IUseCaseJogo _jogoUseCase = useCaseJogo;
     
        [HttpGet("ListarJogos")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            var response = await _jogoUseCase.ListarJogos();
            return response.Ok ? Ok(response) : BadRequest(response);   
        }

        [HttpPost("CriarJogo")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Create(JogoDTO jogo)
        {
           var response = await _jogoUseCase.Criar(jogo);
            return response.Ok ? Ok(response) : BadRequest(response);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id)
        {
            return Ok($"Jogo {id} atualizado com sucesso");
        }
        [HttpDelete("DeletarJogo/{id}")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _jogoUseCase.DeletarJogo(id);
            return response.Ok ? Ok(response) : BadRequest(response);
        }
        [HttpPatch]

        [Produces(typeof(ApiResponse))]
    }
}
