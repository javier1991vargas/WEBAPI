using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApAutores.DTOs;
using WebApAutores.Entidades;

namespace WebApAutores.Controllers
{
    [ApiController]
    [Route("api/libros/{LibroId:int}")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int _libroId)
        {
            var existe = await context.Comentarios.AnyAsync(x => x.LibroID == _libroId);

            if (!existe)
            {
                return NotFound();
            }
            var comentarios = await context.Comentarios.Where(x => x.LibroID == _libroId).ToListAsync();
            return mapper.Map<List<ComentarioDTO>>(comentarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int _libroId, ComentariosCreacionDTO _comentariosCreacionDTO)
        {
            var existe = await context.Libros.AnyAsync(x => x.Id == _libroId);

            if (!existe)
            {
                return NotFound();
            }
            var comentario_ = mapper.Map<Comentarios>(_comentariosCreacionDTO);
            comentario_.LibroID = _libroId;
            context.Add(comentario_);
            await context.SaveChangesAsync();
            return Ok();

        }

    }
}

