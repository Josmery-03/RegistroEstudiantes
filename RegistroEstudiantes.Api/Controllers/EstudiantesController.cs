using Microsoft.AspNetCore.Mvc;
using RegistroEstudiantes.Models;
using RegistroEstudiantes.Services;

namespace RegistroEstudiantes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstudiantesController : ControllerBase
{
    private readonly EstudiantesService _estudiantesService;

    public EstudiantesController(EstudiantesService estudiantesService)
    {
        _estudiantesService = estudiantesService;
    }

    [HttpGet]
    public async Task<ActionResult> GetList()
    {
        var resultado = await _estudiantesService.GetList(e => true);
        return Ok(resultado);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> Buscar(int id)
    {
        var resultado = await _estudiantesService.Buscar(id);
        if (resultado == null)
        {
            return NotFound("Estudiante no encontrado");
        }
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Guardar([FromBody] Estudiantes estudiante)
    {
        estudiante.EstudianteId = 0;
        var resultado = await _estudiantesService.Guardar(estudiante);
        return Ok(resultado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Modificar(int id, [FromBody] Estudiantes estudiante)
    {
        if (id != estudiante.EstudianteId)
        {
            return BadRequest("El Id del estudiante no coincide.");
        }

        var existente = await _estudiantesService.Buscar(id);
        if (existente == null)
        {
            return NotFound("Estudiante no encontrado");
        }

        var resultado = await _estudiantesService.Guardar(estudiante);
        return Ok(resultado);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var resultado = await _estudiantesService.Eliminar(id);
        return Ok(resultado);
    }
}