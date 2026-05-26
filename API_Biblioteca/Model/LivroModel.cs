using System.ComponentModel.DataAnnotations;

namespace API_Biblioteca.Model
{
    public class LivroModel
    {
        public int Id { get; set; }
   
        public string Titulo { get; set; } = string.Empty;

        public int AnoPublicacao { get; set; }

        public StatusLivro StatusLivro { get; set; }


        public int AutorId { get; set; }

        public AutorModel Autor { get; set; } = new AutorModel();


    }

    public enum StatusLivro
    {
        Disponivel = 0,
        Emprestado = 1,
    }
}
