using API_Biblioteca.DTO;
using API_Biblioteca.Model;

namespace API_Biblioteca.Service
{
    public interface IEmprestimoInterface
    {
        public Task<List<EmprestimoModel>> ListarEmprestimos();
        public Task<EmprestimoModel> BuscarEmprestimoPorId(int id);
        public Task<EmprestimoModel> AdicionarEmprestimo(EmprestimoCriacaoDTO emprestimoCriacaoDTO);
        public Task<EmprestimoModel> EditarEmprestimo(int id, EmprestimoEdicaoDTO emprestimoEdicaoDTO);
        public Task<EmprestimoModel> DevolverLivro(int id);
    }
}
