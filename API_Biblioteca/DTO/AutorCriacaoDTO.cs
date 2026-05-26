using API_Biblioteca.Model;
using System.ComponentModel.DataAnnotations;

namespace API_Biblioteca.DTO
{
    public class AutorCriacaoDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do autor é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do autor não pode exceder 100 caracteres.", MinimumLength = 1)]
        public string NomeAutor { get; set; } = string.Empty;
        public ICollection<LivroModel> Livros { get; set; } = new List<LivroModel>();
    }
}
