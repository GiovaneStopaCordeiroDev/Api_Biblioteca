using API_Biblioteca.Enum;
using API_Biblioteca.Model;
using System.ComponentModel.DataAnnotations;

namespace API_Biblioteca.DTO
{
    public class UsuarioCriacaoDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome de usuário não pode exceder 100 caracteres.", MinimumLength = 1)]
        public required string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email deve ser um endereço de email válido")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public required string Senha { get; set; }
        [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
        public required string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "O cargo é obrigatório")]
        public CargoEnum Cargo { get; set; }

    }
}
