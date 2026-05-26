using API_Biblioteca.DTO;
using API_Biblioteca.Model;

namespace API_Biblioteca.Service
{
    public interface ILivroInterface
    {

        public Task<List<LivroModel>> ListarLivros();
        public Task<LivroModel> BuscarLivroPorId(int id);
        public Task<LivroModel> BuscarLivroPorTitulo(string titulo);
        public Task<LivroModel> AdicionarLivro(LivroCriacaoDTO livroCriacaoDTO);
        public Task<LivroModel> EditarLivro(int id, LivroEdicaoDTO livroEdicaoDTO);
        public Task<LivroModel> DeletarLivro(int id);



    }
}
