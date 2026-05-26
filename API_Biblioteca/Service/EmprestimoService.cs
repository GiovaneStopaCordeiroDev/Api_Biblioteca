using API_Biblioteca.Data;
using API_Biblioteca.DTO;
using API_Biblioteca.Errors;
using API_Biblioteca.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API_Biblioteca.Service
{
    public class EmprestimoService : IEmprestimoInterface
    {
        private readonly AppDbContext _context;
        public EmprestimoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<EmprestimoModel> AdicionarEmprestimo(EmprestimoCriacaoDTO emprestimoCriacaoDTO)
        {

                var NovoEmprestimo = new EmprestimoModel
                {
                    LivroId = emprestimoCriacaoDTO.LivroId,
                    UsuarioId = emprestimoCriacaoDTO.UsuarioId,
                };

                var LivroEmprestado = await _context.Emprestimos.AnyAsync(e => e.LivroId == emprestimoCriacaoDTO.LivroId);
                var UsuariosId = await _context.Usuarios.FindAsync(emprestimoCriacaoDTO.UsuarioId);
                var Livros = await _context.Livros.FindAsync(emprestimoCriacaoDTO.LivroId);
               

                if (UsuariosId == null)
                {

                    throw new BadRequestException("Digite o usuário que irá realizar o empréstimo.");

                }
                if (Livros == null)
                {
                    throw new BadRequestException("Digite o livro que irá realizar o empréstimo.");

                }
                if (LivroEmprestado)
                {

                    throw new BadRequestException("O livro já está emprestado, escolha outro livro para realizar o empréstimo.");

                }

                _context.Add(NovoEmprestimo);
                await _context.SaveChangesAsync();
                return NovoEmprestimo;

                throw new BadRequestException($"Ocorreu um erro ao adicionar o empréstimo");

        }

        public async Task<EmprestimoModel> BuscarEmprestimoPorId(int id)
        {

                var Emprestimo = await _context.Emprestimos
                    .Include(e => e.Livro)
                    .Include(e => e.Usuario)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (Emprestimo == null)
                {
                    throw new NotFoundException($"Empréstimo {id} não encontrado.");
                }

                return Emprestimo;

                throw new BadRequestException($"Ocorreu um erro ao buscar o emprestimo");

        }

        public async Task<EmprestimoModel> DevolverLivro(int Id)
        {

                var LivroDevolvido = await _context.Emprestimos.FirstOrDefaultAsync(e => e.Id == Id);
                if (LivroDevolvido == null)
                {
                    throw new NotFoundException($"Empréstimo {Id} não encontrado.");
                }
                _context.Remove(LivroDevolvido);
                await _context.SaveChangesAsync();

           
                return LivroDevolvido;

                throw new BadRequestException($"Ocorreu um erro ao devolver o livro");

        }

        public async Task<EmprestimoModel> EditarEmprestimo(int id, EmprestimoEdicaoDTO emprestimoEdicaoDTO)
        {

                var emprestimoEditado = await _context.Emprestimos.FirstOrDefaultAsync(e => e.Id == id);

                if (emprestimoEditado == null)
                {
                    throw new NotFoundException($"Empréstimo {id} não encontrado.");
                }

                emprestimoEditado.UsuarioId = emprestimoEdicaoDTO.UsuarioId;
                emprestimoEditado.LivroId = emprestimoEdicaoDTO.LivroId;

                await _context.SaveChangesAsync();

                return emprestimoEditado;

                throw new BadRequestException($"Ocorreu um erro ao editar o empréstimo.");

        }

        public async Task<List<EmprestimoModel>> ListarEmprestimos()
        {

                var ListaEmprestimo = await _context.Emprestimos
                    .Include(e => e.Livro)
                    .Include(e => e.Usuario)
                    .ToListAsync();

                return ListaEmprestimo;

                throw new BadRequestException($"Ocorreu um erro ao listar os emprestimos");


        }
    }
}
