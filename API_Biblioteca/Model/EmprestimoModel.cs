using System.ComponentModel.DataAnnotations;

namespace API_Biblioteca.Model
{
    public class EmprestimoModel
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public LivroModel ?Livro { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel ?Usuario { get; set; }

    }
}
