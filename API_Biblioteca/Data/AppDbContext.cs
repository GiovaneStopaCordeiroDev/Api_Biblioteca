using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using API_Biblioteca.Model;

namespace API_Biblioteca.Data
{
    public class AppDbContext : DbContext
    {

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }
        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<EmprestimoModel> Emprestimos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

    }
}
