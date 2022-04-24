namespace WebApAutores.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<Comentarios> comentarios { get; set; }

    }
}
