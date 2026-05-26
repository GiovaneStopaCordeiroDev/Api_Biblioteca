using API_Biblioteca.Model;
using System.ComponentModel.DataAnnotations;

namespace API_Biblioteca.DTO
{
    public class LivroEdicaoDTO
    {
        [Required(ErrorMessage = "O ID do livro é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do livro é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título do livro não pode exceder 200 caracteres.", MinimumLength = 1)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ano de publicação é obrigatório.")]
        public int AnoPublicacao { get; set; }
        [Required(ErrorMessage = "O status do livro é obrigatório.")]
        public StatusLivro StatusLivro { get; set; }
        [Required(ErrorMessage = "O ID do autor é obrigatório.")]
        public int AutorId { get; set; }
    }
}
