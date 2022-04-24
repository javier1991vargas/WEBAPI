using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApAutores.DTOs;
using WebApAutores.Entidades;

namespace WebApAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<LibroDTO>>> Get()
        {
            var libro = await context.Libros.ToListAsync();
            return mapper.Map<List<LibroDTO>>(libro);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
           var libro= await context.Libros.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<LibroDTO>(libro);
        }


        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<LibroDTO>>> get (string nombre)
        {
            var libro = await context.Libros.Where(x => x.Titulo.Contains(nombre)).ToListAsync();
            return mapper.Map<List<LibroDTO>>(libro);
        }
        //[HttpGet("{nombre}")]
        //public async Task<ActionResult<List<AutorDTO>>> Get(string nombre)
        //{
        //    var autor = await Context.Autores.Where(AutorBD => AutorBD.Nombre.Contains(nombre)).ToListAsync();
        //    return mapper.Map<List<AutorDTO>>(autor);
        //}

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            var libro = mapper.Map<Libro>(libroCreacionDTO);

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();

        }



    }
}
