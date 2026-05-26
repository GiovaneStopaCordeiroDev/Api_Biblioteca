using API_Biblioteca.Data;
using API_Biblioteca.DTO;
using API_Biblioteca.Errors;
using API_Biblioteca.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Biblioteca.Service
{
    public class AutorService : IAutorInterface
    {
        private readonly AppDbContext _context;

        public AutorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AutorModel> AdicionarAutor(AutorCriacaoDTO autorCriacaoDTO)
        {

                var novoAutor = new AutorModel()
                {
                    NomeAutor = autorCriacaoDTO.NomeAutor,
                    Livros = autorCriacaoDTO.Livros
                };
                if (novoAutor == null)
                {
                    throw new BadRequestException("Autor não pode ser nulo.");
                }
               
                _context.Autores.Add(novoAutor);
                await _context.SaveChangesAsync();
                return novoAutor;

                throw new Exception("Erro ao adicionar autor");
            
        }

        public async Task<AutorModel> BuscarAutorPorId(int id)
        {
   
                var BuscarAutor = await _context.Autores.FindAsync(id);

                if (BuscarAutor == null)
                {
                    throw new NotFoundException("Autor não encontrado.");
                }
                return BuscarAutor;

                throw new BadRequestException("Erro ao buscar autor com ID");

        }

        public async Task<AutorModel> EditarAutor(int id, AutorEdicaoDTO autorEdicaoDTO)
        {
                var AutorEditado = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);
                if (AutorEditado == null)
                {
                    throw new NotFoundException("Autor não encontrado.");
                }

                AutorEditado.NomeAutor = autorEdicaoDTO.NomeAutor;
                AutorEditado.Livros = autorEdicaoDTO.Livros;

                await _context.SaveChangesAsync();

                return AutorEditado; 

                throw new Exception("Erro ao editar autor");
           
        }

        public async Task<AutorModel> ExcluirAutor(int id)
        {

                var autorExcluido = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);
                if (autorExcluido == null)
                {
                    throw new NotFoundException("Autor não encontrado.");
                }
                _context.Autores.Remove(autorExcluido);
                await _context.SaveChangesAsync();

                throw new Exception("Erro ao excluir autor");
            
        }

        public async Task<List<AutorModel>> ListarAutores()
        {
                var ListarAutor = await _context.Autores.ToListAsync();

                return ListarAutor;

                throw new BadRequestException("Erro ao listar autores");
            
        }
    }
}
