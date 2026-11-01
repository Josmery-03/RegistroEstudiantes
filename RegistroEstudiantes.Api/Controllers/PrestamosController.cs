using Microsoft.AspNetCore.Mvc;
using RegistroEstudiantes.Models;
using RegistroEstudiantes.Services;

namespace RegistroEstudiantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController(PrestamosServices prestamosServices) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Prestamos>>> GetPrestamos()
        {
            return await prestamosServices.Listar(p => true);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamos>> GetPrestamo(int id)
        {
            var prestamo = await prestamosServices.Buscar(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            return prestamo;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostPrestamo(Prestamos prestamo)
        {
            if (await prestamosServices.LibroPrestado(prestamo.LibroId, prestamo.PrestamoId))
            {
                return BadRequest("El libro seleccionado ya se encuentra prestado.");
            }

            return await prestamosServices.Guardar(prestamo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutPrestamo(int id, Prestamos prestamo)
        {
            if (id != prestamo.PrestamoId)
            {
                return BadRequest("El ID del préstamo no coincide.");
            }

            return await prestamosServices.Guardar(prestamo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletePrestamo(int id)
        {
            var eliminado = await prestamosServices.Eliminar(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return true;
        }
    }
}