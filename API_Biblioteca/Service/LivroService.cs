using API_Biblioteca.Data;
using API_Biblioteca.DTO;
using API_Biblioteca.Errors;
using API_Biblioteca.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Biblioteca.Service
{
    public class LivroService : ILivroInterface
    {

            private readonly AppDbContext _context;
        public LivroService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<LivroModel> AdicionarLivro(LivroCriacaoDTO livroCriacaoDTO)
        {

                var livro = new LivroModel()
                {
                    AutorId = livroCriacaoDTO.AutorId,
                    Titulo = livroCriacaoDTO.Titulo,
                    AnoPublicacao = livroCriacaoDTO.AnoPublicacao
                };

                _context.Add(livro);
                await _context.SaveChangesAsync();
                return livro;

                throw new BadRequestException("Erro ao adicionar livro");
           
        }

        public async Task<LivroModel> EditarLivro(int id, LivroEdicaoDTO livroEdicaoDTO)
        {

                var livroEditado = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);

                if (livroEditado == null)
                {
                   throw new NotFoundException("Livro não encontrado");
                }

                livroEditado.Titulo = livroEdicaoDTO.Titulo;
                livroEditado.AnoPublicacao = livroEdicaoDTO.AnoPublicacao;
                livroEditado.AutorId = livroEdicaoDTO.AutorId;

                await _context.SaveChangesAsync(); 
                return livroEditado;

                throw new BadRequestException("Erro ao editar livro");
    
        }

        public async Task<LivroModel> BuscarLivroPorId(int id)
        {

                var livroPesquisado =  await _context.Livros.FindAsync(id);

                if (livroPesquisado == null)
                {
                    
                   throw new NotFoundException("Livro não encontrado");

                }
                
                return livroPesquisado;

                throw new BadRequestException("Erro ao buscar livro por id");

        }

        public async Task<LivroModel> BuscarLivroPorTitulo(string titulo)
        {

                var livroPesquisado = await _context.Livros.Where(l => l.Titulo.Contains(titulo)).FirstOrDefaultAsync();

                if (livroPesquisado == null)
                {
                    throw new NotFoundException("Livro não encontrado");
                }

                return livroPesquisado;
            

               
                throw new BadRequestException("Erro ao buscar livro por título");

        }

        public async Task<LivroModel> DeletarLivro(int id)
        {
                var livroExistente = _context.Livros.FirstOrDefault(l => l.Id == id);

                if (livroExistente == null)
                {
                    throw new NotFoundException("Livro não encontrado");
                }

                _context.Livros.Remove(livroExistente);
                await _context.SaveChangesAsync();
                return livroExistente;

                throw new BadRequestException("Erro ao deletar livro: ");
        }

        public async Task<List<LivroModel>> ListarLivros()
        {   
           
                var Listalivros = await _context.Livros.ToListAsync();
                if (Listalivros == null)
                {
                    throw new NotFoundException("Nenhum livro encontrado");
                }
                return Listalivros;
            
           
                throw new BadRequestException("Erro ao listar livros");

        }
    }
}
