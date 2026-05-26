using System.ComponentModel.DataAnnotations;

namespace API_Biblioteca.Model
{
    public class AutorModel
    {
        public int Id { get; set; }
        public string NomeAutor { get; set; } = string.Empty;
        public ICollection<LivroModel> Livros { get; set; } = new List<LivroModel>();
    }
}
