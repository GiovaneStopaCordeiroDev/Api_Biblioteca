using System.ComponentModel.DataAnnotations;

namespace API_Biblioteca.DTO
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; } = string.Empty;
    }
}
