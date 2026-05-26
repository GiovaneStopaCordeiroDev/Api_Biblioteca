using API_Biblioteca.DTO;
using API_Biblioteca.Model;

namespace API_Biblioteca.Service
{
    public interface IAuthInterface
    {
        Task<UsuarioCriacaoDTO> Registrar(UsuarioCriacaoDTO usuarioRegistro);
        Task<UsuarioModel> Login(UsuarioLoginDTO UsuarioLogin);
    }
}
