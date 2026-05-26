using API_Biblioteca.DTO;
using API_Biblioteca.Model;
using API_Biblioteca.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> ListarUsuarios()
        {
            var usuarios = await _usuarioInterface.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarUsuarioPorId(int id)
        {
            var usuario = await _usuarioInterface.BuscarUsuarioPorId(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> AdicionarUsuario([FromBody] UsuarioCriacaoDTO usuario)
        {
            var novoUsuario = await _usuarioInterface.AdicionarUsuario(usuario);
            return CreatedAtAction(nameof(ListarUsuarios), new { id = novoUsuario.Id }, novoUsuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> EditarUsuario(int id, [FromBody] UsuarioEdicaoDTO usuario)
        {
            var usuarioEditado = await _usuarioInterface.EditarUsuario(id, usuario);
            return Ok(usuarioEditado);
        }

        [HttpDelete("ExcluirUsuario/{id}")]
        public async Task<ActionResult> DeletarUsuario(int id)
        {
            await _usuarioInterface.DeletarUsuario(id);
            return NoContent();
        }
    }
}