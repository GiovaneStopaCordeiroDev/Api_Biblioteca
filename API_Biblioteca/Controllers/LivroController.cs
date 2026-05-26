using Microsoft.AspNetCore.Mvc;
using API_Biblioteca.Model;
using API_Biblioteca.Data;
using API_Biblioteca.Service;
using API_Biblioteca.DTO;

namespace API_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet]
        public async Task<ActionResult<List<LivroModel>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();
            return Ok(livros);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LivroModel>> BuscarLivroPorId(int id)
        {
            var livro = await _livroInterface.BuscarLivroPorId(id);
            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult<LivroModel>> AdicionarLivro([FromBody] LivroCriacaoDTO livro)
        {
            var novoLivro = await _livroInterface.AdicionarLivro(livro);
            return CreatedAtAction(nameof(ListarLivros), new { id = novoLivro.Id }, novoLivro);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LivroModel>> EditarLivro(int id, [FromBody] LivroEdicaoDTO livro)
        {
            var livroAtualizado = await _livroInterface.EditarLivro(id, livro);
            return Ok(livroAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarLivro(int id)
        {
            await _livroInterface.DeletarLivro(id);
            return NoContent();
        }
    }
}