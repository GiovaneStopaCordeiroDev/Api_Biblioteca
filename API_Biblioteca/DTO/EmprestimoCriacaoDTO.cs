using System.ComponentModel.DataAnnotations;

namespace API_Biblioteca.DTO;

public class EmprestimoCriacaoDTO
{
    [Required]
    public int LivroId { get; set; }
    [Required]
    public int UsuarioId { get; set; }
    [Required (ErrorMessage = "O nome do usuário é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do usuário não pode exceder 100 caracteres.", MinimumLength = 1)]
    public string UsuarioNome { get; set; } = string.Empty;
    [Required (ErrorMessage = "O nome do livro é obrigatório.")]
    public string LivroNome { get; set; } = string.Empty;
}