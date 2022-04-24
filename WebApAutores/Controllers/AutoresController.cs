using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApAutores.DTOs;
using WebApAutores.Entidades;

namespace WebApAutores.Controllers
{
    [ApiController]
    [Route("Api/Autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext Context, IMapper mapper)
        {
            this.Context = Context;
            this.mapper = mapper;
        }

        [HttpGet]

        public async Task<List<AutorDTO>> Get()
        {
         var autores= await Context.Autores.ToListAsync();
            return mapper.Map<List<AutorDTO>>(autores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {

            var autores = await Context.Autores.FirstOrDefaultAsync(autorBD=> autorBD.Id==id);
            if (autores == null)
            {
                return NotFound();
            }
            return mapper.Map<AutorDTO>(autores);

        
        }
        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<AutorDTO>>> Get(string nombre)
        {
            var autor = await Context.Autores.Where(AutorBD => AutorBD.Nombre.Contains(nombre)).ToListAsync();
            return mapper.Map<List<AutorDTO>>(autor);
        }

        
        [HttpPost]
        public async Task<ActionResult<Autor>> Post(AutorCreacionDTOs autorCreacionDTOs)
        {


            var autorNombre = await Context.Autores.AnyAsync(x => x.Nombre == autorCreacionDTOs.Nombre);
            if (autorNombre)
            {
                return BadRequest($"Ya existe con el mismo nombre{autorCreacionDTOs.Nombre}");
            }

            var autor = mapper.Map<Autor>(autorCreacionDTOs);
            Context.Add(autor);
            await Context.SaveChangesAsync();
            return Ok();
        }


        //[HttpPost]
        //para crear uruarios en la base de datos
        //public async Task<ActionResult> post(AutorCreacionDTOs autor)
        //{
        //    Context.Add(autor);
        //    await Context.SaveChangesAsync();
        //    return Ok();
        //}

        [HttpPut("{id:int}")]
        //ACTUALIZAMOS REGISTRO DE LA BASE DE DATOS
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("el autor no coincide con la url infresada");
            }

            Context.Update(autor);
            await Context.SaveChangesAsync();
            return Ok();
        }

    }
}
