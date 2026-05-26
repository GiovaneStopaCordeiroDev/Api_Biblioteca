using API_Biblioteca.DTO;
using API_Biblioteca.Errors;
using API_Biblioteca.Service;
using Microsoft.AspNetCore.Mvc;

namespace API_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorInterface _autorInterface;
        public AutorController(IAutorInterface autorInterface)
        {
            _autorInterface = autorInterface;
        }


        [HttpGet]
        public async Task<ActionResult> ListarAutores()
        {
            var resposta = await _autorInterface.ListarAutores();
            return Ok(resposta);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarAutorPorId(int id)
        {
            var resposta = await _autorInterface.BuscarAutorPorId(id);
            return Ok(resposta);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarAutor(AutorCriacaoDTO autorCriacaoDTO)
        {
            var resposta = await _autorInterface.AdicionarAutor(autorCriacaoDTO);
            return CreatedAtAction(nameof(BuscarAutorPorId), new { id = resposta.Id }, resposta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarAutor(int id, AutorEdicaoDTO autorEdicaoDTO)
        {
            var resposta = await _autorInterface.EditarAutor(id, autorEdicaoDTO);
            return Ok(resposta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirAutor(int id)
        {
            await _autorInterface.ExcluirAutor(id);
            return NoContent();
        }

    }
}
