using Microsoft.AspNetCore.Mvc;
using RegistroEstudiantes.Models;
using RegistroEstudiantes.Services;

namespace RegistroEstudiantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController(LibrosServices librosServices) : ControllerBase
    {
        
       [HttpGet]
        public async Task<ActionResult<List<Libros>>> GetLibros()
        {
            return await librosServices.Listar(l => true);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Libros>> GetLibro(int id)
        {
            var libro = await librosServices.Buscar(id);
            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostLibro(Libros libro)
        {
            if (await librosServices.ExtisteTitulo(libro.Titulo, libro.LibroId))
            {
                return BadRequest("Ya existe un libro registrado con este mismo título.");
            }

            return await librosServices.Guardar(libro);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutLibro(int id, Libros libro)
        {
            if (id != libro.LibroId)
            {
                return BadRequest("El ID del libro no coincide.");
            }

            return await librosServices.Guardar(libro);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteLibro(int id)
        {
            var eliminado = await librosServices.Eliminar(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return true;
        }
    }
}