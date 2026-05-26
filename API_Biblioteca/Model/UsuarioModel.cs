using API_Biblioteca.Enum;
using System.ComponentModel.DataAnnotations;

namespace API_Biblioteca.Model
{
    public class UsuarioModel
    {
        [Required]
        public int Id { get; set; }

        public required string NomeUsuario { get; set; }

        public required string Email { get; set; }
        public byte[] SenhaHash { get; set; } = [];

        public byte[] SenhaSalt { get; set; } = [];
        public CargoEnum Cargo { get; set; }


    }
}
