using Microsoft.EntityFrameworkCore;
using WebApAutores.Entidades;

namespace WebApAutores
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Autor> Autores{ get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
    }
}
