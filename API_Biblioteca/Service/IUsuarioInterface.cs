using API_Biblioteca.DTO;
using API_Biblioteca.Model;

namespace API_Biblioteca.Service
{
    public interface IUsuarioInterface
    {
       public Task<List<UsuarioModel>> ListarUsuarios();
       public Task<UsuarioModel> BuscarUsuarioPorId(int id);
        public Task<UsuarioModel> AdicionarUsuario(UsuarioCriacaoDTO usuarioCriacaoDTO);
        public Task<UsuarioModel> EditarUsuario(int id, UsuarioEdicaoDTO usuarioEdicaoDTO);
        public Task<UsuarioModel> DeletarUsuario(int id);
    }
}
