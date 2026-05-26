using API_Biblioteca.Data;
using API_Biblioteca.DTO;
using API_Biblioteca.Model;
using API_Biblioteca.Errors;
using Microsoft.EntityFrameworkCore;

namespace API_Biblioteca.Service
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
            public UsuarioService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UsuarioModel> AdicionarUsuario(UsuarioCriacaoDTO usuarioCriacaoDTO)
        {

                var UsuarioNovo = new UsuarioModel
                {
                    Id = usuarioCriacaoDTO.Id,
                    NomeUsuario = usuarioCriacaoDTO.NomeUsuario,
                    Email = usuarioCriacaoDTO.Email,
                };
                if (UsuarioNovo == null)
                {
                    throw new BadRequestException("O usuário não pode ser nulo.");
                }

                _context.Add(UsuarioNovo);
                await _context.SaveChangesAsync();
                return UsuarioNovo;

               
                throw new Exception("Erro ao adicionar usuário");

        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {

                var UsuarioEncontrado = await _context.Usuarios.FindAsync(id);
                if (UsuarioEncontrado == null)
                {
                    throw new NotFoundException("Usuário não encontrado.");
                }

                return UsuarioEncontrado;

                throw new Exception("Erro ao buscar usuário por ID");

        }

        public async Task<UsuarioModel> DeletarUsuario(int id)
        {

            try
            {
                var UsuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
                if (UsuarioExistente == null)
                {
                    throw new NotFoundException("Usuário não encontrado.");
                }

                _context.Remove(UsuarioExistente);
                await _context.SaveChangesAsync();
                return UsuarioExistente;
            }
            catch( Exception ex ) 
            {
                throw new Exception("Erro ao deletar usuário: " + ex.Message);
            }
        }



        public async Task<UsuarioModel> EditarUsuario(int id, UsuarioEdicaoDTO usuarioEdicaoDTO)
        {

                var UsuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (UsuarioExistente == null)
                {
                    throw new NotFoundException("Usuário não encontrado.");
                }

                UsuarioExistente.NomeUsuario = usuarioEdicaoDTO.NomeUsuario;
                UsuarioExistente.Email = usuarioEdicaoDTO.Email;


                await _context.SaveChangesAsync();

                return UsuarioExistente;
            
            throw new BadRequestException("Erro ao editar usuário");

        }

        public async Task<List<UsuarioModel>> ListarUsuarios()
        {


                var UsuariosLista = await _context.Usuarios.ToListAsync();
                if (UsuariosLista.Count == 0)
                {
                   throw new NotFoundException("Nenhum usuário encontrado.");
                }

                return UsuariosLista;

                throw new BadRequestException("Erro ao listar usuários");
            
        }
    }
}
