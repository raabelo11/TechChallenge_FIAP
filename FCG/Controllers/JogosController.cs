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


        /// <summary>
        /// End point para listar todos os jogos cadastrados.
        /// </summary>
        /// <response code="200">Deve retornar lista de jogos.</response>
        [AllowAnonymous]
        [HttpGet("ListarJogos")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            var response = await _jogoUseCase.ListarJogos();
            return response.Ok ? Ok(response) : BadRequest(response);
        }
        [HttpGet("CategoriasJogos")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> ListarCategoriasJogos()
        {

        }
        /// <summary>
        /// End point para cadastrar o jogo
        /// </summary>
        /// <response code="200">Deve deixar cadastrar apenas os usuários com permissão de Administrador.</response>
        /// <response code="401">Não tem permissão.</response>
        /// 
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("CriarJogo")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Create(JogoDTO jogo)
        {
            var response = await _jogoUseCase.Criar(jogo);
            return response.Ok ? Ok(response) : BadRequest(response);
        }
        /// <summary>
        /// End point para inserir desconto no jogo.
        /// </summary>
        /// <response code="200">Deve deixar cadastrar desconto os usuários com permissão de Administrador.</response>
        /// <response code="401">Não tem permissão.</response>
        /// 
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("InserirDesconto/{id}")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<ActionResult<ApiResponse>> Update(int desconto, Guid id)
        {

            var response = await _jogoUseCase.AtualizarJogo(id, desconto);
            return response.Ok ? Ok(response) : BadRequest(response);
        }
        /// <summary>
        /// End point para deletar o jogo.
        /// </summary>
        /// <response code="200">Deve deixar apenas usuários com permissão de Administrador.</response>
        /// <response code="401">Não tem permissão.</response>
        /// 
        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeletarJogo/{id}")]
        [Produces(typeof(ApiResponse))]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _jogoUseCase.DeletarJogo(id);
            return response.Ok ? Ok(response) : BadRequest(response);
        }
    }
}
