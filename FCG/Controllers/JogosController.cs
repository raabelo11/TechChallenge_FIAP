using FCG.Application.Interfaces;
using FCG.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly IUseCaseJogo _jogoUseCase;
        public JogosController(IUseCaseJogo jogo)
        {
            _jogoUseCase = jogo;
        }
        [HttpGet("ListarJogos")]
        public async Task<ActionResult<Jogos>> Get()
        {
            var jogos = await _jogoUseCase.ListarJogos();
            return Ok(jogos);
        }

        [HttpPost]
        public IActionResult Create()
        {
            return CreatedAtAction(nameof(Index), new { id = 1 }, "Jogo criado com sucesso");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id)
        {
            return Ok($"Jogo {id} atualizado com sucesso");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Jogo {id} deletado com sucesso");
        }
    }
}
