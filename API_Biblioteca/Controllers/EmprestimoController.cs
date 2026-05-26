using API_Biblioteca.DTO;
using API_Biblioteca.Errors;
using API_Biblioteca.Service;
using Microsoft.AspNetCore.Mvc;

namespace API_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoInterface _emprestimoInterface;

        public EmprestimoController(IEmprestimoInterface emprestimoInterface)
        {
            _emprestimoInterface = emprestimoInterface;
        }

        [HttpGet]
        public async Task<ActionResult> ListarEmprestimos()
        {
            var emprestimos = await _emprestimoInterface.ListarEmprestimos();
            return Ok(emprestimos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarEmprestimoPorId(int id)
        {
            var emprestimo = await _emprestimoInterface.BuscarEmprestimoPorId(id);
            return Ok(emprestimo);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarEmprestimo(EmprestimoCriacaoDTO emprestimoCriacaoDTO)
        {
            var novoEmprestimo = await _emprestimoInterface.AdicionarEmprestimo(emprestimoCriacaoDTO);
            return CreatedAtAction(nameof(BuscarEmprestimoPorId), new { id = novoEmprestimo.Id }, novoEmprestimo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmprestimos(int id)
        {
            await _emprestimoInterface.DevolverLivro(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarEmprestimo(int id, EmprestimoEdicaoDTO emprestimoEdicaoDTO)
        {
            var resultado = await _emprestimoInterface.EditarEmprestimo(id, emprestimoEdicaoDTO);
            return Ok(resultado);
        }
    }
}
