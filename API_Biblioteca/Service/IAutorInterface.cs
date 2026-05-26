using API_Biblioteca.DTO;
using API_Biblioteca.Model;

namespace API_Biblioteca.Service
{
    public interface IAutorInterface
    {
        public Task<List<AutorModel>> ListarAutores();
        public Task<AutorModel> BuscarAutorPorId(int id);
        public Task<AutorModel> AdicionarAutor(AutorCriacaoDTO autorCriacaoDTO);
        public Task<AutorModel> EditarAutor(int id, AutorEdicaoDTO autorEdicaoDTO);
        public Task<AutorModel> ExcluirAutor(int id);
    }
}
